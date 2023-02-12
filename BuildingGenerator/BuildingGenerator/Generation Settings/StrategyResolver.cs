using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.BuildingGenerator.Generation_Settings
{
    public static class StrategyResolver
    {

        public static WingsStrategy ResolveWingsStratFromName(string wingsStratName)
        {
            switch (wingsStratName)
            {
                case "DefaultWingsStrategy": return new DefaultWingsStrategy();
                case "RandomizedCountWingsStrategy": return new RandomizedCountWingsStrategy();
                default: throw new Exception("Could not resolve Wings Strategy from name!");
            }
        }

        public static WingStrategy ResolveWingStratFromName(string wingStratName)
        {
            switch (wingStratName)
            {
                case "DefaultWingStrategy": return new DefaultWingStrategy();
                default: throw new Exception("Could not resolve Wing Strategy from name!");
            }
        }

        public static StoriesStrategy ResolveStoriesStratFromName(string storiesStratName)
        {
            switch (storiesStratName)
            {
                case "DefaultStoriesStrategy": return new DefaultStoriesStrategy();
                case "RandomizedCountStoriesStrategy": return new RandomizedCountStoriesStrategy();
                case "RandomizedCountStoriesWithTerracesStrategy": return new RandomizedCountStoriesWithTerracesStrategy();
                default: throw new Exception("Could not resolve Stories Strategy from name!");
            }
        }

        public static StoryStrategy ResolveStoryStratFromName(string storyStratName)
        {
            switch (storyStratName)
            {
                case "DefaultStoryStrategy": return new DefaultStoryStrategy();
                default: throw new Exception("Could not resolve Story Strategy from name!");
            }
        }

        public static WallsStrategy ResolveWallsStratFromName(string wallsStratName)
        {
            switch (wallsStratName)
            {
                case "DefaultWallsStrategy": return new DefaultWallsStrategy();
                case "ParametrizedWallsStrategy": return new ParametrizedWallsStrategy();
                case "MultiStoryParametrizedWallsStrategy": return new MultiStoryParametrizedWallsStrategy();
                default: throw new Exception("Could not resolve Walls Strategy from name!");
            }
        }

        public static RoofStrategy ResolveRoofStratFromName(string roofStratName)
        {
            switch (roofStratName)
            {
                case "DefaultRoofStrategy": return new DefaultRoofStrategy();
                default: throw new Exception("Could not resolve Roof Strategy from name!");
            }
        }

        public static List<string> GetAllWingsStrats()
        {
            return new List<string>()
            {
                "DefaultWingsStrategy",
                "RandomizedCountWingsStrategy"
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
                "DefaultStoryStrategy"
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
                "DefaultRoofStrategy"
            };
        }
    }
}
