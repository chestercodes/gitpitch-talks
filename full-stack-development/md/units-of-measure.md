
## Units of measure

![MarsRover](assets/img/marsRover.jpg)

@quote[Units-of-measure are to science what types are to programming.]

---

@quote[NASA's $125 million Mars Climate Orbiter project ended in failure when the orbiter dipped 90 km closer to Mars than originally intended, causing it to tear apart and disintegrate spectacularly in the Mars atmosphere. ... the root cause of the problem to a conversion error ... NASA passed data to the systems in metric units, but the software expected data in Imperial units.]

--- 

### Scenario

Usage and conversion of degrees and radians

![Radians](assets/img/rads.png)


---?code=code/uom/rad.fsx&lang=fs

@[1-2](Define units of measure, radian and degree)
@[1-5](Assign values of different measures)
@[7](Conversion factor to convert between measures.)
@[4-10](Can add twoRad to oneHundredDeg with conversion)
@[12](Trying to add values doesn't compile )
