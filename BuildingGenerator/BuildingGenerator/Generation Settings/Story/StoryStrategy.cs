using System.Collections;
using System.Collections.Generic;

public abstract class StoryStrategy
{
    public abstract Story GenerateStory(BuildingSettings settings, GenerationParams genParams);
}
