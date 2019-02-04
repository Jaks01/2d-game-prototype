# OpenTK Documentation

### Use OpenGL API:
OpenTK wraps OpenGL in static class, so to use OpenGl api like `glViewport`, do `GL.View` port instead.

Some enums from OpenGL such as `GL_ARRAY_BUFFER` becomes `BufferTarget.ArrayBuffer`, following the format of `EnumName.EnumValue`

# OVERRIDES

### Open Game Window:
class can inherit from `GameWindow`, then pass options through that class

```C#
class MainWindow : GameWindow {

	new MainWindow (//custom assign value params ) 
	 	: base ( //assign initial optional window values) 
	{ 	//assign params
	 	Title += “ GL VERSION : ” …
	}
}
```

In game loop, call run to open window:

```C#
void Main(){
	new MainWindow(//pass in custom params).Run(60.0, 0.0);
 	//params in Run() ensures that the game logic update runs at the same speed  
 	//of the machine 
 	//performance varies 
}
```

### Resize Game Window:
when `GameWindowFlag.Default`, means user can resize game window. To reset viewport when window is resized.

```C#
override void OnResize(EventArgs e){
	GL.Viewport(0, 0, Width, Height);	
 	//x and y to 0, w and h of viewport to window dimens
 	//(0, 0) lower left corner of screen
}
```

`OnResize` get called whenever window dimension changes

### Initialize:
when the game window opens, the method OnLoad gets called. Use it to initialize

```C#
override void OnLoad(){
	//do stuff
}
```

### Updates
`OnUpdateFrame` updates every frame, so update the world in here

```C#
Override void OnUpdateFrame(FrameEventArgs e){
	//update stuff
}
```

### Rendering
also gets called every frame like `OnUpdateFrame`. Put all the rendering in here

```C#
override void OnRenderFrame(FrameEventArgs e) {
	//render stuff
	e.Time //from FrameEvebtArgs gives the elapsed time of the window

 	SwapBuffer();	//shows the rendered scene to user on screen
}
```

# COMPLING SHADER & LINKING

For object to be rendered, it uses shaders, but first we need to create them

### Creating Shader
The bare minimum to get something to the screen are using `VertexShader` and `FragmentShader`. The procedure to load both of them are the same.

**VertexShader**: handles individual vertices’ positions, rotations, etc. being fed attribute data to set variables within the shader

```C#
#version 450 core

layout (location = 0) in vec4 position;
layout(location = 1) in vec4 color;
out vec4 vs_color;

layout (location = 20) uniform mat4 projection;
layout (location = 21) uniform mat4 modelView;

void main(void)
{
 gl_Position = projection * modelView * position;
 vs_color = color;
}
```

**FragmentShader**: the shader that process the fragments when creating / rasterizing a shape into a set of colors and a single depth value.

```C#
#version 450 core
in vec4 vs_color;
out vec4 color;

void main(void)
{
 color = vs_color;
}
```
