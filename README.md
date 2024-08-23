# Text Rendering
Definition of generated [meshes](https://github.com/game-simulations/meshes) with UV coordinates pointing to glyphs from [fonts](https://github.com/game-simulations/fonts).

### Usage
Text rendering happens in 3D space like rendering other meshes. Each of them require a text mesh, material
and a camera (similar to regular renderers). The text mesh itself requires a font and the text content.
```cs
using World world = new();
Font font = ...
TextMesh textMesh = new(world, "Hello, World!", font);
while (!textMesh.Is())
{
    world.Submit(new DataUpdate());
    world.Submit(new FontUpdate());
    world.Submit(new RenderUpdate());
    world.Poll();
}

Material textMaterial = ...
TextRenderer textRenderer = new(world, textMesh, textMaterial, camera);
Entity textRendererEntity = textRenderer;
```
> The material is expected to have a sampler2D at binding 0 for the font atlas. The remaining is up to you.