using BuildingGenerator.Shared;
using System.Collections;
using System.Collections.Generic;

public class RandomizedCountStoriesWithTerracesStrategy : StoriesStrategy
{
    public override Story[] GenerateStories(BuildingSettings settings, GenerationParams genParams)
    {
        Random random = new Random();
        float storiesRnd = random.Next(1, settings.buildingStoryCount);
        int storiesInt = (int)Math.Round(storiesRnd);

        Story[] stories = new Story[storiesInt];
        
        for (int i = 0; i < storiesInt; i++)
        {
            genParams.StoryNumber = i;
            if (genParams.StoryNumber == 0)
            {
                stories[i] = settings.storyStrategy != null
                    ? settings.storyStrategy.GenerateStory(settings, genParams)
                    : new DefaultStoryStrategy().GenerateStory(settings, genParams);
            }
            else
            {
                var newSizeX = (int)Math.Round((decimal)random.Next(1, settings.Bounds.xMax));
                var newSizeY = (int)Math.Round((decimal)random.Next(1, settings.Bounds.yMax));
                var newPosX = (int)Math.Round((decimal)random.Next(genParams.BoundingBox.x-newSizeX+1, genParams.BoundingBox.xMax-1));
                var newPosY = (int)Math.Round((decimal)random.Next(genParams.BoundingBox.y-newSizeY+1, genParams.BoundingBox.yMax-1));
                // TODO: a felsőbb szintek boundingboxát valahogy randomizálni, esetleg kivezetni egy kapcsolót a felületre
                
                genParams.BoundingBox = new RectInt(new Vector2Int(newPosX, newPosY), new Vector2Int(newSizeX,newSizeY));
                stories[i] = settings.storyStrategy != null
                    ? settings.storyStrategy.GenerateStory(settings, genParams)
                    :  new DefaultStoryStrategy().GenerateStory(settings, genParams);
            }

        }

        return stories;
    }
}
