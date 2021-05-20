# Games Engines 2 Assignment 
 World underwater with fish boids

This assignment is an underwater world, full of underwater creatures moving automounsly using steering behaviours
There isn't much a story, but it is planned to have the camera move between different creatures in the scene, having fish move 
in flocks wandering and having obstacle avoidance. 

So this project contains the following

# IMPORTANT NOTE
Most code audio and prefabs are found with in Assets/Games Engines 2/Scripts
rest are majority downloaded assets

# Pirate Ship Battle

Pirate ships uses noise wander to move, and uses state machines to handle what happens when the come near each other, which is rotate and fire cannons.
When the health of one ship has been depleted, the camera will move to the ship, it will then sink and eventually be destroyed, releasing particles

# Flocks
all fish are within flocks, using flocking behaviour, the combination of cohesion, seperation and alignment

# Rocks
rocks have colorful corals on it, uses harmonics to move with particle effects

# Sharks
The shark follows a path, any fish nearby it will pursue until it's too far away (uses state machines)

# Crabs
crab uses harmonic waves to move about, as well as follwing a path generated by a * to move while avoiding the rocks

# Dolphin
Follows a path with followers using offest puruse, if the camera is focused on it and the user presses p, it will seek to the top of the water, and do a flip and go back to following the path

# Diver
Uses behaviour trees to manage it's transitions. it will pursue a fish and shoot it to kill it. dead fish will be upside down silightly floating up and down. once near a dead fish, the fish will be captured in the diver's net and then the diver will seek his boat before purseing another fish. diver uses harmonics to move his legs and arm.

# Blue Fish
uses flow field path follwing. the flow field is generated using perlin noise, it will follwing the flow field while seeking a target that moves when the fish get near

# Controls
Camera static until pirate ship destroyed,
FPSController at first, so free movement
Press P to enter camera follow, which focuses on specific creatures,
Press L to alterante between creatures
Press E to go back to FPScontroller
Press P while focused on dolphins to make them flip

# Classes wrote myself
- Steering Behaviours : Flocking.cs (creates the flock), FlockingBehaviour.cs (cohesion,seperation,alignment, influenced by lecture notes)
- Flowfield code : Flowfieldgrid.cs (adapted from nature of code https://natureofcode.com/book/chapter-6-autonomous-agents/), FollowFlowfield.cs ( wrote myself )

# What I'm most proud of
I'm really proud of how the scene looks and the behaviours used to implement the scene. My favrioute behaviour is the flow field, the perlin noise flow field really makes it cool to watch the fish transverse. Flocking is another behaviour that's really cool, seeing fish move like that, and I am proud I managed to get it done before it was taught in class. Although not perfect, I'm proud of exploring new techniques such as behaviour trees and understand how they work, even though I didn't implement it completely right I hope to study it further and use it to create complex behaviours in the future. Overall I'm really glad how it turned out, I spent alot of time and it really turned out cool

# Youtube video link 
https://www.youtube.com/watch?v=uhwxESHknUY
