// NOTE: This tutorial is made of fragments... a better one will follow!


// These references are required only for typechecking purposes
// you can skip their evaluation.
#r "ViewletControls.dll"
#r "VSLabFSICore.dll"

open System
open System.Drawing
open System.Windows.Forms

open VSLab

// let's create some viewlet..

// a simple function plotter 
let t2 = new plotter()
t2.Show()
// let's try some function
t2.func <- (fun a -> a/2)
t2.func <- (fun a -> a*2)
t2.func <- (fun a -> a*3-a*a/100)
t2.func <- (fun a -> 300/(a+1))
let r = new System.Random()
t2.func <- (fun a -> (r.Next(a)))
t2.Invalidate()

// a silly segment drawer to show mouse support
let t3 = new liner()
t3.Show()

// a viewlet to show the clipping rectangle
let simpleViewlet = new clipper()
simpleViewlet.Show()
// example repainting an area 
simpleViewlet.Invalidate(new Rectangle(50, 50, 150, 150))
// now repainting an area overlapping with the former
simpleViewlet.Invalidate(new Rectangle(25, 25, 100, 100))
// now repaint everything
simpleViewlet.Invalidate()
// now close the viewlet
simpleViewlet.Close()

// a bouncing ball...
let test_ball = new ball(Velo=5)
test_ball.Show()
test_ball.Velo <- 10
test_ball.Velo <- 6
test_ball.Velo <- 2
test_ball.Close()
// using the keyboard..
let k = new test_key()
k.Show()


open VSLab.Chart3D
let viewer1 = new Function3DViewlet(Name="first 3D function")
let viewer2 = new Function3DViewlet(Name="second 3D function")
viewer1.Show()
viewer2.Show()

let mutable v = 0.1
viewer1.Function <- fun (t:float) (x, y) -> sin(t)*v*(x-y)
viewer2.Function <- fun (t:float) (x, y) -> sin(t)*v*(x+y)
v <- 0.5
v <- 1.0

viewer1.Function <- fun (t:float) (x, y) -> sin(t+x+y) + x*y + y*x
viewer2.Function <- fun (t:float) (x, y) -> sin(t+x+y)*v 

viewer1.Close()
viewer2.Close()


// ------ Viewlets Manager --------
// how many viewlets do we have?
Viewlets.Count
// let's see the viewlets list
Seq.iter (fun (v:Viewlet) -> printf "%s\n" v.Name) Viewlets.Items
// a function to find a viewlet by its name
let FindViewlet n = Seq.find (fun (v:Viewlet) -> v.Name = n) Viewlets.Items
// let's get a viewlet
let found = FindViewlet "first 3D function"


// save viewlets state
Viewlets.Save("test.xml")

// close all viewlets
Array.iter (fun (v:Viewlet) -> v.Close() |> ignore) (Seq.to_array Viewlets.Items)

// load it back
Viewlets.Load("test.xml")

// how to write a simple viewlet..
type simpleViewlet() as x = 
  inherit Viewlet()
  override x.OnPaint(e) =
    e.Graphics.DrawRectangle(Pens.Red, new Rectangle(10,10,50,50))

let simple = new simpleViewlet()
simple.Show()


// Example of multiple balls animated

open System.Drawing
open System.Windows.Forms

[<Measure>] type px
[<Measure>] type s

open VSLab

let ballsz = new SizeF(25.0f, 25.0f)

type Ball(ipx: float<px>, ipy:float<px>, ivx:float<px/s>, ivy:float<px/s>) =
  let mutable vx = ivx
  let mutable vy = ivy

  let mutable px = ipx
  let mutable py = ipy  
  let sz = ballsz

  member x.Update(interval:float<s>) =
    px <- px + vx * interval
    py <- py + vy * interval

  member x.Size
    with get() = sz

  member x.Position 
    with get() = (px, py)
    
  member x.CenterPosition
    with get() = ((px + 1.0<px> * (float (sz.Width / 2.0f))), (py + 1.0<px> * (float (sz.Height / 2.0f))))
    
  member x.Velocity
    with get() = (vx, vy)
    and set (v:float<px/s>*float<px/s>) =
      vx <- fst v
      vy <- snd v

type SampleViewlet() as x =
  inherit Viewlet()

  let balls = new ResizeArray<Ball>()

  let framerate = 0.02<s>

  let t = new Timer()

  let mutable newball:Option<Ball> = None
  let mutable mousepos = (0, 0)
  
  do
    t.Interval <- int(framerate * 1000.0)
    t.Tick.Add(fun _ ->
      balls.ToArray() |> Array.iter (fun b ->
        b.Update(framerate)
        let px, py = b.Position
        let pxi = (int px)
        let pyi = (int py)
        let mutable vx, vy = b.Velocity
    
        if (pxi < 0 || pxi > x.ClientSize.Width - (int b.Size.Width)) then
          vx <- vx * -1.0

        if (pyi < 0 || pyi > x.ClientSize.Height - (int b.Size.Height)) then
          vy <- vy * -1.0
    
        b.Velocity <- (vx, vy)
      )
  
      x.Invalidate()
    )
    t.Start()
    x.Text <- "Animate!"

  override x.OnMouseDown e =
    mousepos <- (e.X, e.Y)    
    let px = 1.0<px>*((float e.X) - ((float ballsz.Width) / 2.0))
    let py = 1.0<px>*((float e.Y) - ((float ballsz.Height) / 2.0))
    let b = new Ball(px, py, 0.0<px/s>, 0.0<px/s>)
    balls.Add(b)
    newball <- Some(b)

  override x.OnMouseMove e =
    if newball.IsSome then
      mousepos <- (e.X, e.Y)    

  override x.OnMouseUp e =
    if newball.IsSome then
      let px, py = newball.Value.Position
      let vx = (float (fst mousepos - (int px)))*1.0<px/s>
      let vy = (float (snd mousepos - (int py)))*1.0<px/s>
      newball.Value.Velocity <- (vx, vy)
      newball <- None

  override x.OnPaint e =
    let g = e.Graphics
    g.SmoothingMode <- Drawing2D.SmoothingMode.AntiAlias
    balls.ToArray() |> Array.iter (fun b ->
      let px, py = b.Position
      g.FillEllipse(Brushes.Blue, (single px), (single py), ballsz.Width, ballsz.Height))

    if newball.IsSome then
      let px, py = newball.Value.CenterPosition
      let ipx = (int px)
      let ipy = (int py)
      let mx, my = mousepos
      use p = new Pen(Color.Black)
      p.EndCap <- Drawing2D.LineCap.ArrowAnchor
      g.DrawLine(p, ipx, ipy, mx, my)
  
  override x.OnViewletVisibilityChange e =
    t.Enabled <- e

let bv = new SampleViewlet()
bv.Show()
bv.Close()