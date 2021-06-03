# Code Test

03/06/2021

## Sources

### Industry.Simulation

I used a deterministic maths library I created a couple of weeks ago to power the game simulation.

- Fixed-point number
- Fixed-point Vector2s
- Fixed-point boxes
- Fixed-point mathmatics

The libraries source code is currently not public on my GitHub; but I hope to make it public soon.

![Deterministic Maths](https://i.gyazo.com/f5bd1f360a08b46cec505c58a715e389.gif)

I could have used `System.Numerics` (as it is .NET Standard compatible), but if I wanted to use `UnityEngine.Vector2` then I wouldn't be able to make the game outside of Unity.

### RPGCore.Events

RPGCore.Events is an event-system API I created as apart of the [RPGCore project](https://github.com/Fydar/RPGCore). It allows for an alternative to `INotifyPropertyChanged` for model change notifications.

### Newtonsoft.Json

I am also using `Newtonsoft.Json` to power my serialization. I would have used `System.Text.Json` if it was more compatible with Unity (hopefully it will be soon. I've got some shims that make it work but I haven't tested it enough yet). Of course, I could have used Unity's default `JsonUtility`, but `Newtonsoft.Json` has a lot more features.
