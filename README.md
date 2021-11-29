# Marble Eater

Marble Eater is a first-person indoor casual game primarily meant for AR smartphones.\
A horde of marbles are unleashed and are eating each other. The only way to stop them is\
to eat them and become the biggest marble. 

## Unity Asset
Playable Scene: MarbleEaterGame.unity\
The Asset includes aparticle System that continously creates marbles, a world boundary and a player marble.\
In the final game, the world boundary is intended to be dynamically created by scanning the room.\
This can be accomplished using the LIDAR sensor of an apple smartphone.
For this asset, we use a prefab giving world boundaries.

The Particle system mainly generates Enemy marbles. These marbles can eat and be eaten by all other marbles.\
The Player marble is NOT part of the particle system. The player directly controls this marble and can hold and "flick" the marble towards other enemy marbles.\
The player can recall their marble to their position at any point in time. While recall or holding the player marble, there wont be any collision detection. 

The Enemy Marbles are color coded according to the size of the player marble.
- Red Marbles are larger than the player, can eat you. 
- Green Marbles are smaller than the player, can be eaten.
- Blue Marbles are of the same size as the player, nothing happens.

The enemy color changes as the player size increases. New enemy marbles spawn after any marble is eaten, thus
keeping the total number of marbles in the system consistent.\ 
The player marble has limited lives. A Life is lost once they are Eaten by an enemy marble\
When All lives are lost, the game ends. Life counter is present in the GUI and emphazied in the game world\


## Controls
WASD to move\
Hold left mouse buttom to Hold marble.\
If ball not held, holding left mouse button will summon marble towards the player.\
Release mouse to propel the marble forward.

Same controls will be adapted for mobile devices.\
Directional touchpad for player movement.\
Long-press for grabbing marble, release to shoot.

##
What a thrill\
With darkness and silence through the night\
What a thrill\
I'm searching and I'll melt into you\
What a fear in my heart\
But you're so supreme!

I give my life\
Not for honor, but for you\
In my time, there'll be no one else\
Crime, it's the way I fly to you \
I'm still in a dream.. marble eater

I am stillll in a dream... MARBLE EATER!\
(Lyrics taken from Opening song of MGS3: Snake Eater)
