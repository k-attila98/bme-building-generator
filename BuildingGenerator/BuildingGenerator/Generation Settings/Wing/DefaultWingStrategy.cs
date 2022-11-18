using System.Collections;
using System.Collections.Generic;

public class DefaultWingStrategy : WingStrategy
{
    public override Wing GenerateWing(BuildingSettings settings, GenerationParams genParams)
    {
        return new Wing(
            genParams.BoundingBox,
            settings.storiesStrategy != null ?
                settings.storiesStrategy.GenerateStories(settings, genParams) : 
                new DefaultStoriesStrategy().GenerateStories(settings, genParams),
            (settings.roofStrategy != null ? 
                settings.roofStrategy.GenerateRoof(settings, genParams) : 
                new DefaultRoofStrategy().GenerateRoof(settings, genParams)
            )
        );
    }
}
