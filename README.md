## About
LibRbxl is software library targeting .NET 4.5 and that provides an easy way to read and write Roblox .rbxl files.

## Usage Examples
Add a script to an existing place.
```csharp
var doc = RobloxDocument.FromFile("Example.rbxl");
var serverScriptService = doc:FindFirstChild("ServerScriptService");
var script = new Script();
script.Source = "print(\"Hello, World!\")";
script.Parent = serverScriptService;
doc.Save("Example.rbxl");
```
Open a file and count the number of parts.
```csharp
var doc = RobloxDocument.FromFile("Example2.rbxl");
var partCount = doc.Instances.Count(n => n Is Part);
```
Create document from scratch.
```csharp
var doc = new RobloxDocument();
var workspace = new Workspace();
doc.Children.Add(workspace);
var model = new Model();
model.Name = "ExampleModel";
model.Parent = workspace; // Setting the parent property automatically adds the instance as a child of the parent
doc.Save("Example3.rbxl");
```
## Build
LibRbxl is a stand-alone DLL. To compile it, you will need Visual Studio. Clone the repository locally and build it using the solution file.
## Contribute
Contributions are welcome! If you're not inclined to dig into the code yourself, but encounter an issue, feel free to submit an issue using the GitHub issue tracker.
## Major Open Tasks
* Serialized/Deserializer Optimization - Thanks to [DanDevPC](https://github.com/DanDevPC), serializer performance has improved substantially from earlier versions. However, there is more work to be done. More aggressive caching is needed to reduce the cost of using reflections. Many instances of LINQ code can be replaced with imperative code in performance-sensitive areas.
* Solid Modeling - Currently, solid modeling data is supported only through unmanaged properties (no parsing is done). Further work is needed to understand the format of solid modeling data and support it through the use of strongly typed properties on Instance classes.
* Terrain - Similar to Solid Modeling, no parsing is done on terrain data. Further understanding of the format of terrain data is needed to implement proper support for the terrain data in the library.
* Additional Instance Types and Properties - While the majority of Roblox classes and properties have a matching .NET class in the LibRbxl.Instances namespace, a number do not, particularly those that do not show up in the Roblox Studio Object Browser. These need to be documented and implemented.

## License
LibRbxl is open source, and is licensed under the MIT License. The full text is included in the repository.
