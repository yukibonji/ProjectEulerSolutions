// These references are required only for typechecking purposes
// you can skip their evaluation.
#r "ViewletControls"
#r "VSLabFSICore"
#r "Microsoft.DirectX.Direct3D"
#r "VSLabChart3DPackage"
#r "EnvDTE80"


open VSLab.Chart3D

let viewer1 = new Function3DViewlet(Name="first 3D function")
let viewer2 = new Function3DViewlet(Name="second 3D function")
let viewer3 = new Function3DViewlet(Name="262")
viewer1.Show()
viewer2.Show()

let mutable v = 0.1
viewer1.Function <- fun (t:float) (x, y) -> sin(t)*v*(x-y)
viewer2.Function <- fun (t:float) (x, y) -> sin(t)*v*(x+y)
v <- 0.5
v <- 1.0

viewer1.Function <- fun (t:float) (x, y) -> sin(t+x+y) + x*y + y*x
viewer2.Function <- fun (t:float) (x, y) -> sin(t+x+y)*v 

let h x y = (5000.0-0.005*(x*x+y*y+x*y)+12.5*(x+y)) * exp(-abs(0.000001*(x*x+y*y)-0.0015*(x+y)+0.7))

viewer3.Show()

viewer3.Function <- fun (t:float) (x, y) -> h x y

viewer1.Close()
viewer2.Close()


