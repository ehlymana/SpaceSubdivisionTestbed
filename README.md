# SpaceSubdivisionTestbed

*SpaceSubdivisionTestbed* is a desktop graphical tool intended for automatically subdividing different types of 2D polygons. The application was created by using .NET 6 framework and the Windows Forms template type.

If you use this application, please cite the following work:

```
Emir Cogo and Ehlimana Krupalija and Selma Rizvić, 2023. "SpaceSubdivisionTestbed: A graphical tool for arbitrary shaped 2D polygon subdivision", CSAE '23: Proceedings of the 7th International Conference on Computer Science and Application Engineering, Virtual event, China, pp. 1-5, DOI: 10.1145/3627915.3629605.
```

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

![](https://github.com/ehlymana/SpaceSubdivisionTestbed/blob/main/README%20figures/initialUI.png)

### 1. Drawing polygons

Drawing polygons is done by adding polygon edges to the panel. All edges must be added in a counter-clockwise order with any starting point. If the user reverses the order, they can always use the *Invert* button from the *Subdivision* groupbox to reverse the order a second time.

To initiate drawing, the user first needs to click on the *Start drawing* button. Polygons can be drawn on the panel in two ways:

1. **Graphical drawing**, which allows the user to draw a polygon by using their pointer on the panel. After starting the drawing, the user needs to click on the desired location in order to add a new node described by X and Y coordinates. After adding the initial node, the current pointer location is shown on the left side of the *Graphical drawing* groupbox (X and Y coordinates). An example of graphical drawing is shown below.

![](https://github.com/ehlymana/SpaceSubdivisionTestbed/blob/main/README%20figures/drawpolygon.gif)

2. **Manual insertion**, which allows the user to manually specify the X and Y coordinates of all nodes by using the *Manual insertion* groupbox and the *Insert* button. An example of manual insertion is shown below.

![](https://github.com/ehlymana/SpaceSubdivisionTestbed/blob/main/README%20figures/manualdraw.gif)

The process of drawing a polygon is finished when the final node is equal to the starting node. In order to achieve this, the user can either graphically or manually specify this node (and create the final edge) and then click on the *Finish drawing* button. Alternatively, the user can click on the *Close loop* button which will automatically add the final edge and finish drawing the polygon.

### 2. Working with polygons

a) **Polygon identification**

After the polygon is successfully drawn, its edges are colored in a randomly determined color in order to make the distinguishing from other polygons easier. However, sometimes multiple polygons can have similar colors, so it can be necessary to find out the ID tags of each polygon.

In order to identify a polygon, the *Identify element* button from the *Subdivision* groupbox needs to be clicked. After hovering the pointer over the desired polygon, a tooltip with the ID of the polygon will be shown. The ID and the color of the polygon can now be used to correctly identify the polygon for further usage. A demonstration of this feature can be found in the example for performing polygon subdivision below.

b) **Polygon subdivision**

After selecting the desired existing polygon and clicking on the *Begin subdivision* button from the *Subdivision* groupbox, polygon subdivision will be performed. The subdivision works on arbitrary shape types and always subdivides the polygon in half, by using the longer dimension (X or Y). An example of polygon subdivision and identification is shown below.

![](https://github.com/ehlymana/SpaceSubdivisionTestbed/blob/main/README%20figures/polygonsubdivision.gif)

c) **Determining shape type**

Each polygon belongs to one of four shape categories: rectangular, axis-aligned, convex, or irregular. After the user selects the desired polygon and clicks on the *Determine shape* button from the *Subdivision* groupbox, the text box below the controls will show information about the shape type of the input polygon. A demonstration of this feature is shown below.

![](https://github.com/ehlymana/SpaceSubdivisionTestbed/blob/main/README%20figures/shapetype.gif)

d) **Polygon area calculation**

After selecting the desired polygon, the user can click on the *Calculate area* button from the *Subdivision* groupbox. This will calculate the area which the polygon occupies, expressed in pixels (e.g. if a rectangle is 100 by 100 pixels long, it occupies an area of 1,000 pixels). An example of area calculation is shown below.

![](https://github.com/ehlymana/SpaceSubdivisionTestbed/blob/main/README%20figures/area.gif)

e) **Polygon simplification**

Iterative subdivision can be performed on a single polygon. The subdivision stops either when all subdivided polygons are of rectangular shape, or after the 10th iteration is finished. To perform polygon simplification, the user needs to click on the *Simplify* button from the *Subdivision* groupbox. Iterative subdivision will be performed on the desired polygon and information about the overall area of leftover convex elements will be shown in a message box. An example of polygon simplification is shown below.

![](https://github.com/ehlymana/SpaceSubdivisionTestbed/blob/main/README%20figures/simplification.gif)

### 3. Data persistence

The *Import/export data* button is located at the top right corner of the tool. All polygons which were drawn and/or subdivided can be exported and then imported for later usage in the tool. The data is saved in JSON format, and many examples of all shape types are located in the following directory:

```
SpaceSubdivisionTestbed/Examples
```

The JSON format can also be used for programmatically adding new polygons without having to draw them manually. An example of importing and exporting data is shown below.

![](https://github.com/ehlymana/SpaceSubdivisionTestbed/blob/main/README%20figures/importexport.gif)

### 4. Reset

The user can click on the *Reset* button, located at the right bottom corner of the panel, at any time to delete all existing polygons from the panel and start the process of drawing and subdivision over again. An example of this feature is shown below.

![](https://github.com/ehlymana/SpaceSubdivisionTestbed/blob/main/README%20figures/reset.gif)
