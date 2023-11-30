using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BuildingGenerator.BuildingGenerator.Generation_Settings
{
    public static class StrategyResolver
    {
        public static WingsStrategy ResolveWingsStratFromName(string wingsStratName)
        {
            if (!GetAllWingsStrats().Contains(wingsStratName))
            {
                throw new ArgumentException("Could not resolve Wings Strategy from name!");
            }

            var type = Type.GetType(wingsStratName);

            if (type == null)
            {
                throw new TypeAccessException("Could not resolve Wings Strategy from name!");
            }

            var myObject = (WingsStrategy)Activator.CreateInstance(type);
            return myObject ?? new DefaultWingsStrategy();
        }

        public static WingStrategy ResolveWingStratFromName(string wingStratName)
        {

            if (!GetAllWingStrats().Contains(wingStratName))
            {
                throw new ArgumentException("Could not resolve Wing Strategy from name!");
            }

            var type = Type.GetType(wingStratName);

            if (type == null)
            {
                throw new TypeAccessException("Could not resolve Wing Strategy from name!");
            }

            var myObject = (WingStrategy)Activator.CreateInstance(type);
            return myObject ?? new DefaultWingStrategy();
        }

        public static StoriesStrategy ResolveStoriesStratFromName(string storiesStratName)
        {

            if (!GetAllStoriesStrats().Contains(storiesStratName))
            {
                throw new ArgumentException("Could not resolve Stories Strategy from name!");
            }

            var type = Type.GetType(storiesStratName);

            if (type == null)
            {
                throw new TypeAccessException("Could not resolve Stories Strategy from name!");
            }

            var myObject = (StoriesStrategy)Activator.CreateInstance(type);
            return myObject ?? new DefaultStoriesStrategy();
        }

        public static StoryStrategy ResolveStoryStratFromName(string storyStratName)
        {

            if (!GetAllStoryStrats().Contains(storyStratName))
            {
                throw new ArgumentException("Could not resolve Story Strategy from name!");
            }

            var type = Type.GetType(storyStratName);

            if (type == null)
            {
                throw new TypeAccessException("Could not resolve Story Strategy from name!");
            }

            var myObject = (StoryStrategy)Activator.CreateInstance(type);
            return myObject ?? new DefaultStoryStrategy();
        }

        public static WallsStrategy ResolveWallsStratFromName(string wallsStratName)
        {
            if (!GetAllWallsStrats().Contains(wallsStratName))
            {
                throw new ArgumentException("Could not resolve Walls Strategy from name!");
            }

            var type = Type.GetType(wallsStratName);
            
            if (type == null)
            {
                throw new TypeAccessException("Could not resolve Walls Strategy from name!");
            }

            var myObject = (WallsStrategy)Activator.CreateInstance(type);
            return myObject ?? new DefaultWallsStrategy();
        }

        public static RoofStrategy ResolveRoofStratFromName(string roofStratName)
        {
            if (!GetAllRoofStrats().Contains(roofStratName))
            {
                throw new ArgumentException("Could not resolve Roof Strategy from name!");
            }

            var type = Type.GetType(roofStratName);

            if (type == null)
            {
                throw new TypeAccessException("Could not resolve Roof Strategy from name!");
            }

            var myObject = (RoofStrategy)Activator.CreateInstance(type);
            return myObject ?? new DefaultRoofStrategy();
        }

        public static List<string> GetAllWingsStrats()
        {
            return new List<string>()
            {
                "DefaultWingsStrategy",
                "RandomizedCountWingsStrategy",
                "TShapedWingsStrategy",
                "ZigzagShapedWingsStrategy",
                "UShapedWingsStrategy"
            };
        }

        public static List<string> GetAllWingStrats()
        {
            return new List<string>()
            {
                "DefaultWingStrategy"

            };
        }

        public static List<string> GetAllStoriesStrats()
        {
            return new List<string>()
            {
                "DefaultStoriesStrategy",
                "RandomizedCountStoriesStrategy",
                "RandomizedCountStoriesWithTerracesStrategy"
            };
        }

        public static List<string> GetAllStoryStrats()
        {
            return new List<string>()
            {
                "DefaultStoryStrategy",
                "HangingFlooredStoryStrategy"
            };
        }

        public static List<string> GetAllWallsStrats()
        {
            return new List<string>()
            {
                "DefaultWallsStrategy",
                "ParametrizedWallsStrategy",
                "MultiStoryParametrizedWallsStrategy"
            };
        }

        public static List<string> GetAllRoofStrats()
        {
            return new List<string>()
            {
                "DefaultRoofStrategy",
                "ProceduralPeakRoofStrategy"
            };
        }
    }
}
