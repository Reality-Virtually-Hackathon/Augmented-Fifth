# Augmented-Fifth

Our team studied spatio-temporal effects on 3D-rendered animated objects mapped with sound sequencers for augmented reality. The goal was to create your own musical world where you can program characters to play music and see the audio effects mapped spatially and temporally. 

Pipeline:
  -Character Sketch and Animation (Adobe Illustrator and Blender)
  -AugmentedReality and object tracking (Apple ARkit and Unity)
  -Sound synthesizer (Abelton)
  
Our demo was an Iphone/Ipad AR app that consisted of placing computer rendered 3D animals (lizard, flamingo, and manatee) in the real-world. The user would be able to place these animals on tables, desks, etc and give each of them a musical melody. As the user moves around the room, they hear the combination of all the musical melodies of the animals. Also, when they approach an animal the audio of that animal gets louder and the audio of the other animals get faint. 

These objects, once sketched in Adobe Illustrator and animated in Blender, were attached the real world using Apple AR Toolkit and Unity. Each object was also given an unique animation (lizard moved it's mouth, flamingo moved it's body, manatee clapped it's hands).  Once animations and object tracking were working, we moved to integrate our custom built sound sequencers. Each animal object was assigned an unique sound sequencer (bells, drums, piano, etc) which the user could change to make new songs. 

Under MIT License.
