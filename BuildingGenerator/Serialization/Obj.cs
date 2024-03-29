﻿using BuildingGenerator.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BuildingGenerator.Serialization
{
    public class Obj
    {
        private List<Vertex> _vertices = new List<Vertex>();
        private List<Face> _faces = new List<Face>();
        private List<TextureVertex> _textureVertices = new List<TextureVertex>();
        public string UseMtl { get; set; }
        public string Mtl { get; set; }

        public Extent Size { get; set; }

        public string LoadPath { get; set; }

        public Obj(){  }

        public Obj(List<Transform> placedPrefabs)
        {
            foreach (var placedPrefab in placedPrefabs)
            {
                foreach (var face in placedPrefab.Faces)
                {
                    foreach (var vertex in face.Vertices)
                    {
                        _vertices.Add(vertex);
                        vertex.Id = VertexIdProvider.GetNextId();
                        
                    }

                    foreach (var textureVertex in face.TextureVertices)
                    {
                        _textureVertices.Add(textureVertex);
                        textureVertex.Id = _textureVertices.Count;
                    }
                    _faces.Add(face);
                }
            }
            Mtl = placedPrefabs.ElementAt(0).Mtl;
            UseMtl = placedPrefabs.ElementAt(0).UseMtl;
        }

        private void _WriteHeader(StreamWriter writer, string[] headerStrings)
        {
            if (headerStrings == null || headerStrings.Length == 0)
            {
                return;
            }
            foreach (var line in headerStrings)
            {
                writer.WriteLine("# " + line);
            }
        }

        public void WriteObjFile(string[] headerStrings)
        {
            using (StreamWriter writer = new StreamWriter(File.Open($"../../../Generated/building-{DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss")}.obj", System.IO.FileMode.Create)))
            {
                // Write some header data
                _WriteHeader(writer, headerStrings);

                if (!string.IsNullOrEmpty(Mtl))
                {
                    writer.WriteLine("mtllib " + Mtl);
                }

                _vertices.ForEach(v => writer.WriteLine(v));
                _textureVertices.ForEach(tv => writer.WriteLine(tv));
                string lastUseMtl = "";
                foreach (Face face in _faces)
                {
                    if (face.UseMtl != null && !face.UseMtl.Equals(lastUseMtl))
                    {
                        writer.WriteLine("usemtl " + face.UseMtl);
                        lastUseMtl = face.UseMtl;
                    }
                    writer.WriteLine(face);
                }
            }
        }

        public string WriteObjString(string[] headerStrings)
        {
            string ret;
            using (var strStream = new MemoryStream())
            {
                var writer = new StreamWriter(strStream);

                // Write some header data
                _WriteHeader(writer, headerStrings);

                if (!string.IsNullOrEmpty(Mtl))
                {
                    writer.WriteLine("mtllib " + Mtl);
                }
                
                _vertices.ForEach(v => writer.WriteLine(v));
                _textureVertices.ForEach(tv => writer.WriteLine(tv));

                string lastUseMtl = "";
                foreach (Face face in _faces)
                {
                    if (face.UseMtl != null && !face.UseMtl.Equals(lastUseMtl))
                    {
                        writer.WriteLine("usemtl " + face.UseMtl);
                        lastUseMtl = face.UseMtl;
                    }
                    writer.WriteLine(face);
                }

                writer.Flush();
                strStream.Position = 0; 


                var reader = new StreamReader(strStream);
                
                ret = reader.ReadToEnd();

                reader.Dispose();
                writer.Dispose();
                
            }

            return ret;
            
        }

        public void LoadObj(string path)
        {
            this.LoadPath = path;
            LoadObj(File.ReadAllLines(path));
        }

        public void LoadObj(Stream data)
        {
            using (var reader = new StreamReader(data))
            {
                LoadObj(reader.ReadToEnd().Split(Environment.NewLine.ToCharArray()));
            }
        }

        public void LoadObj(IEnumerable<string> data)
        {
            foreach (var line in data)
            {
                _ProcessLine(line);
            }

            _UpdateSize();
        }

        private void _UpdateSize()
        {
            // If there are no vertices then size should be 0.
            if (_vertices.Count == 0)
            {
                Size = new Extent
                {
                    XMax = 0,
                    XMin = 0,
                    YMax = 0,
                    YMin = 0,
                    ZMax = 0,
                    ZMin = 0
                };

                // Avoid an exception below if VertexList was empty.
                return;
            }

            Size = new Extent
            {
                XMax = _vertices.Max(v => v.Position.x),
                XMin = _vertices.Min(v => v.Position.x),
                YMax = _vertices.Max(v => v.Position.y),
                YMin = _vertices.Min(v => v.Position.y),
                ZMax = _vertices.Max(v => v.Position.z),
                ZMin = _vertices.Min(v => v.Position.z)
            };
        }

        private void _ProcessLine(string line)
        {
            string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 0)
            {
                switch (parts[0])
                {
                    case "usemtl":
                        UseMtl = parts[1];
                        break;
                    case "mtllib":
                        Mtl = parts[1];
                        break;
                    case "v":
                        Vertex v = new Vertex();
                        v.LoadFromStringArray(parts);
                        _vertices.Add(v);
                        v.Id = _vertices.Count();
                        break;
                    case "f":
                        Face f = new Face();
                        f.LoadFromStringArray(parts);
                        f.UseMtl = UseMtl;
                        _faces.Add(f);
                        break;
                    case "vt":
                        TextureVertex vt = new TextureVertex();
                        vt.LoadFromStringArray(parts);
                        _textureVertices.Add(vt);
                        vt.Id = _textureVertices.Count();
                        break;

                }
            }
        }

        public Vertex GetVertexById(int id)
        {
            return _vertices[id - 1];
        }

        public TextureVertex GetTextureVertexById(int id)
        {
            return _textureVertices[id - 1];
        }

        public Transform ToTransform()
        {
            var transform = new Transform();
            var facesWithProperVertices = new List<Face>();
            foreach (var face in _faces)
            { 
                var newFace = new Face();
                foreach (var vertex in face.Vertices)
                {
                    var properVertex = _vertices.Single(v => v.Id == vertex.Id);
                    newFace.AddVertex(properVertex);
                }
                foreach (var textureVertex in face.TextureVertices)
                {
                    var properTextureVertex = _textureVertices.Single(tv => tv.Id == textureVertex.Id);
                    newFace.AddTextureVertex(properTextureVertex);
                }
                facesWithProperVertices.Add(newFace);
            }
            transform.Faces = facesWithProperVertices.ToArray();
            transform.Position = new Vector3(0, 0, 0);
            transform.Width = (float)(Size.XMax - Size.XMin);
            transform.Height = (float)(Size.YMax - Size.YMin);
            transform.Mtl = Mtl;
            transform.UseMtl = UseMtl;
            transform.Size = Size;
            transform.LoadPath = LoadPath;

            return transform;
        }
    }
}
