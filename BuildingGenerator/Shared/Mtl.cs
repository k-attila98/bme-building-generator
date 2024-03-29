﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Shared
{
    public class Mtl
    {
        public List<Material> MaterialList;

        /// <summary>
        /// Constructor. Initializes VertexList, FaceList and TextureList.
        /// </summary>
        public Mtl()
        {
            MaterialList = new List<Material>();
        }

        /// <summary>
        /// Load .obj from a filepath.
        /// </summary>
        /// <param name="file"></param>
        public void LoadMtl(string path)
        {
            LoadMtl(File.ReadAllLines(path));
        }

        /// <summary>
        /// Load .obj from a stream.
        /// </summary>
        /// <param name="file"></param>
	    public void LoadMtl(Stream data)
        {
            using (var reader = new StreamReader(data))
            {
                LoadMtl(reader.ReadToEnd().Split(Environment.NewLine.ToCharArray()));
            }
        }

        /// <summary>
        /// Load .mtl from a list of strings.
        /// </summary>
        /// <param name="data"></param>
	    public void LoadMtl(IEnumerable<string> data)
        {
            foreach (var line in data)
            {
                _ProcessLine(line);
            }
        }

        public void WriteMtlFile(string path, string[] headerStrings)
        {
            using (var outStream = File.OpenWrite(path))
            using (var writer = new StreamWriter(outStream))
            {
                // Write some header data
                WriteHeader(writer, headerStrings);

                MaterialList.ForEach(v => writer.WriteLine(v));
            }
        }

        private void WriteHeader(StreamWriter writer, string[] headerStrings)
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

        private Material currentMaterial()
        {
            if (MaterialList.Count > 0) return MaterialList.Last();
            return new Material();
        }

        /// <summary>
        /// Parses and loads a line from an OBJ file.
        /// Currently only supports V, VT, F and MTLLIB prefixes
        /// </summary>		
        private void _ProcessLine(string line)
        {
            string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 0)
            {
                Material CurrentMaterial = currentMaterial();
                Color c = new Color();
                switch (parts[0])
                {
                    case "newmtl":
                        CurrentMaterial = new Material();
                        CurrentMaterial.Name = parts[1];
                        MaterialList.Add(CurrentMaterial);
                        break;
                    case "Ka":
                        c.LoadFromStringArray(parts);
                        CurrentMaterial.AmbientReflectivity = c;
                        break;
                    case "Kd":
                        c.LoadFromStringArray(parts);
                        CurrentMaterial.DiffuseReflectivity = c;
                        break;
                    case "Ks":
                        c.LoadFromStringArray(parts);
                        CurrentMaterial.SpecularReflectivity = c;
                        break;
                    case "Ke":
                        c.LoadFromStringArray(parts);
                        CurrentMaterial.EmissiveCoefficient = c;
                        break;
                    case "Tf":
                        c.LoadFromStringArray(parts);
                        CurrentMaterial.TransmissionFilter = c;
                        break;
                    case "Ni":
                        CurrentMaterial.OpticalDensity = float.Parse(parts[1]);
                        break;
                    case "d":
                        CurrentMaterial.Dissolve = float.Parse(parts[1]);
                        break;
                    case "illum":
                        CurrentMaterial.IlluminationModel = int.Parse(parts[1]);
                        break;
                    case "Ns":
                        CurrentMaterial.SpecularExponent = float.Parse(parts[1]);
                        break;
                }
            }
        }

    }
}
