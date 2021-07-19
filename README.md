# Event-System
Simple Event System that I used in most of my games. It promotes modularity and gets rid of dependencies.
Years ago I got base code from Quil18 in this video: https://youtu.be/04wXkgfd9V8 and modified it for my needs.

## Workflow
The only dependency here is that EventSystem must be attached to GameObject in a scene.

Other objects are separated into two categories. Callers and listeners. Callers fire events and listeners listen to those events. But they don't know each other. 
Caller just says: This event with this info is fired! My job is done, handle the rest... 
EventSystem browse registered listeners and say to each one: I am obliged to tell you, your event is fired. 
And Listener will just scream: "This is what I was waiting for!" and do what he is designed to do in a callback function.

This is why it promotes modularity. Because anyone can register for each event and anyone can fire it. Rest is handled by EventSystem.

Better example will be with Event PlayerDied. In beginning scene UI with DeathScreen will register for this event. Life goes on until it doesn't. PlayerController detects that health felt under 0 and fire event PlayerDied. UI will get called and show DeathScreen with possibility to restart.
But now we want to make camera fade into black. So we tell CameraController to register to this event and in callback, we fade camera. 
No need to change anything in PlayerController or in UI. Beautiful.

## Example
There is ExampleScene with basic example. There is Caller script that will fire EventExample. If you press Space it will fire with ID 1 and if you press A it will fire with ID 2. 
Listener will listen to these events and Log them.
When there is Event with ID 1 functions TestListen, TestListenerLead will be called. 
When there is Event with ID 2 also function TestListenWithFilter will be called, because in registering we used lambda to receive only events with ID = 2;

## Performance
Biggest drawback. I created stress test scene. I spawned 1000 gameobjects with circle sprites. Each will listen to event and when event is fired they will move in random direction. 
If I run it through EventSystem when caller fire event every 0.1f second FPS drop to 100. If I change it, that each listener will run Coroutine where they also move every 0.1f second FPS stay over 1000. Quite differnce. 

But this is for 1000 gameobjects called in same frame. That doesn't really happen. If it will happen in future, I will deal with it then. 

If you want to try stress test, load test scene runs it, check profiler. Then stop it and in Spawner change toggle SelfMove, run it again and compare profiler.
