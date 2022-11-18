using System.Collections;
using System.Collections.Generic;

public class RandomizedCountStoriesStrategy : StoriesStrategy
{
    public override Story[] GenerateStories(BuildingSettings settings, GenerationParams genParams)
    {
        float storiesRnd = new Random().Next(1, settings.buildingStoryCount);

        int storiesInt = (int)Math.Round(storiesRnd);
        
        Story[] stories = new Story[storiesInt];
        
        for (int i = 0; i < storiesInt; i++)
        {
            genParams.StoryNumber = i;
            stories[i] = settings.storyStrategy != null
                ? settings.storyStrategy.GenerateStory(settings, genParams)
                : new DefaultStoryStrategy().GenerateStory(settings, genParams);
            
        }

        return stories;
    }
}