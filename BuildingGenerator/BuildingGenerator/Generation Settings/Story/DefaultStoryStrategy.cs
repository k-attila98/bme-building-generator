using System.Collections;
using System.Collections.Generic;
public class DefaultStoryStrategy : StoryStrategy
{
    public override Story GenerateStory(BuildingSettings settings, GenerationParams genParams)
    {
        return new Story(
            genParams.StoryNumber,
            settings.wallsStrategy != null ? 
                settings.wallsStrategy.GenerateWalls(settings, genParams) :
                new DefaultWallsStrategy().GenerateWalls(settings, genParams),
            genParams.BoundingBox
            );
    }
}
