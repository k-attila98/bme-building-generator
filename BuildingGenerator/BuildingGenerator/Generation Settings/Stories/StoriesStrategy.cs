using System.Collections;
using System.Collections.Generic;

public abstract class StoriesStrategy
{
    public abstract Story[] GenerateStories(BuildingSettings settings, GenerationParams genParams);
}
