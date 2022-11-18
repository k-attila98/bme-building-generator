using System.Collections;
using System.Collections.Generic;
public class DefaultStoriesStrategy : StoriesStrategy
{
    public override Story[] GenerateStories(BuildingSettings settings, GenerationParams genParams)
    {
        return new Story[]
        {
            settings.storyStrategy != null ?
                settings.storyStrategy.GenerateStory(settings, genParams) :
                new DefaultStoryStrategy().GenerateStory(settings, genParams)
        };
    }
}
