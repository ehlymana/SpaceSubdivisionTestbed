# SpaceSubdivisionTestbed

*SpaceSubdivisionTestbed* is a desktop graphical tool intended for subdividing different types of 2D space. The application was created by using .NET 6 framework and the Windows Forms template type.

If you use this application, please cite the following work:

> TBA

## Usage

The executable version of the application is located in the following directory:

```
SpaceSubdivisionTestbed/Executable/SpaceSubdivisionTestbed.exe
```

The application can be directly executed on the Microsoft Windows operating system after installing the [.NET 6 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0). For Linux users, [Wine](https://www.winehq.org) needs to be installed in order to be able to execute the file. For Mac users, the source code needs to be built manually by using tools such as [Microsoft Visual Studio](https://visualstudio.microsoft.com/vs/mac/).

The full source code of the application is located in the following directory:

```
SpaceSubdivisionTestbed/SpaceSubdivisionTestbed/SpaceSubdivisionTestbed.sln
```

## Features

The user interface of the application is shown below. It consists of four main features which will be explained next.

![](https://github.com/ehlymana/SpaceSubdivisionTestbed/README figures/initial_UI.png)

### 1. Drawing shapes

To initiate drawing, the user first needs to click on the *Start drawing* button. Shapes can be drawn in two ways:

1. **Graphical drawing**, which allows the user to draw a shape by using their pointer on the panel. After starting the drawing, the user needs to click on the desired location in order to add a new node described by X and Y coordinates. After adding the initial node, the current pointer location is shown on the left side of the *Graphical drawing* groupbox.

An example of graphical drawing is shown below.

![](https://raw.githubusercontent.com/ehlymana/SpaceSubdivisionTestbed/main/README%20figures/draw.gif?token=GHSAT0AAAAAACAD4E35CTWKEJSUKEQ7QSVEZF6YTGA)

2. **Manual insertion**, which allows the user to manually specify the X and Y coordinates of all nodes by using the *Manual insertion* groupbox and the *Insert* button.

An example of manual insertion is shown below.

![](https://github.com/ehlymana/SpaceSubdivisionTestbed/README figures/manual.gif)

The process of drawing a shape is finished when the final node is equal to the starting node. In order to achieve this, the user can either graphically or manually specify this node (and create the final edge) and then click on the *Finish drawing* button. Alternatively, the user can click on the *Close loop* button which will automatically add the final edge and finish drawing the shape.

### 2. Working with shapes

a) Shape identification

After the shape is successfully drawn, its edges are colored in a random color in order to make the distinguishing from other shapes easier. However, sometimes multiple shapes can have similar colors, so it can be necessary to find out the ID tags of each shape.

In order to identify a shape, the *Identify element* button from the *Subdivision* groupbox needs to be clicked. After hovering the pointer over the desired shape, a tooltip with the ID of the shape will be shown. The ID and the color of the shape can now be used to correctly identify the shape for further usage. A demonstration of this feature can be found in the next subsection.

b) Shape subdivision

After selecting the desired existing shape and clicking on the *Begin subdivision* button from the *Subdivision* groupbox, shape subdivision will be performed. The subdivision works on arbitrary shape types and always subdivides the shape in half, by using the longer dimension (X or Y).

An example of shape subdivision and identification is shown below.

![](https://github.com/ehlymana/SpaceSubdivisionTestbed/README figures/subdivision.gif)

c) Determining shape type

Each shape belongs to one of four categories: rectangular, axis-aligned, convex, or irregular. After the user selects the desired shape and clicks on the *Determine shape* button from the *Subdivision* groupbox, the text box below the controls will show information about the type of the input shape. A demonstration of this feature can be found in the next subsection.

Several types of data can be identified for a single shape:

d) Shape area calculation

After selecting the desired shape, the user can click on the *Calculate area* button from the *Subdivision* groupbox. This will calculate the area which the shape occupies, expressed in pixels (e.g. if a rectangle is 100 by 100 pixels long, it occupies an area of 1,000 pixels).

An example of shape subdivision and identification is shown below.

![](https://github.com/ehlymana/SpaceSubdivisionTestbed/README figures/shape.gif)

### 3. Data persistence

The *Import/export data* button is located at the top right corner of the tool. All shapes which were drawn and/or subdivided can be exported and then imported for later usage in the tool. The data is saved in JSON format, and many examples of all shape types are located in the following directory:

```
SpaceSubdivisionTestbed/Examples
```

The JSON format can also be used for programmatically adding new shapes without having to draw them manually.

### 4. Reset

The user can click on the *Reset* button, located at the right bottom corner of the panel, at any time to delete all existing shapes from the panel and start the process of drawing and subdivision over again.
