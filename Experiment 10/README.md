# Motion Tracking with Matchmover Using Blender

This project demonstrates motion tracking using Blender to match 3D objects with live-action footage. The workflow focuses on tracking camera motion and applying it to Blender’s 3D environment.

---

## Steps

### Step 1: Prepare Your Video Clip
- Choose a video clip with clear, high-contrast features for tracking.

<div align="center">
<img src="https://github.com/user-attachments/assets/edeaa370-748f-49e4-bc38-8304144cbded" width="70%" style="border:1px solid #ccc; padding:3px;"/>
</div>

---

### Step 2: Set Up Blender for Motion Tracking
1. **Open Blender**: Switch to the **Motion Tracking** workspace from the top bar.

<div align="center">
<img src="https://github.com/user-attachments/assets/5d71df52-184b-420a-be98-deec1be938c7" width="60%" style="border:1px solid #ccc; padding:3px;"/>
</div>

2. **Load the Video**:
   - Open the **Movie Clip Editor** → Click **Open** → Load your video file.

<div align="center">
<img src="https://github.com/user-attachments/assets/dd6cca76-512f-434a-9375-ecf7f238b9e0" width="70%" style="border:1px solid #ccc; padding:3px;"/>
</div>

---

### Step 3: Track Features in the Video
1. **Set Up Markers**:
   - Click **Detect Features** to automatically place tracking markers, or manually place markers by holding **Ctrl + Click** on distinct points.
<div align="center">
<img src="https://github.com/user-attachments/assets/7366bf25-18be-4bfc-8bae-8014c1fd4593" width="40%" style="border:1px solid #ccc; padding:3px;"/>
</div>


<div align="center">
<img src="https://github.com/user-attachments/assets/ac47942a-fe77-4aef-a283-cb6cfa864614" width="30%" style="border:1px solid #ccc; padding:3px;"/>
</div>

2. **Track Motion**:
   - Click **Track Forward (▶)** to track each marker through the frames.
     <div align="center">
     <img src="https://github.com/user-attachments/assets/378276b6-5cf0-4d94-9b43-a85fe04204bb" width="50%" style="border:1px solid #ccc; padding:3px;"/>
     </div>
   - Adjust marker settings (like search size) if tracking fails.

<div align="center">

<img src="https://github.com/user-attachments/assets/7366bf25-18be-4bfc-8bae-8014c1fd4593" width="40%" style="border:1px solid #ccc; padding:3px;"/>
<img src="https://github.com/user-attachments/assets/8e6dba96-e023-468e-9ed7-c6f73cba40fa" width="60%" style="border:1px solid #ccc; padding:3px;"/>
</div>

---

### Step 4: Solve the Camera Motion
1. **Set Up Keyframes**:
   - Go to the **Solve** tab in the sidebar and set two keyframes where camera motion is distinct.

<div align="center">
<img src="https://github.com/user-attachments/assets/cc0db43b-33af-45eb-a8c4-847c91c971b7" width="35%" style="border:1px solid #ccc; padding:3px;"/>
</div>

2. **Solve Camera Motion**:
   - Click **Solve Camera Motion** and check the **Solve Error** (should be under 1 pixel for accuracy).

<div align="center">
<img src="https://github.com/user-attachments/assets/a054fdf5-0dea-45a7-a223-a16f98a38bfa" width="60%" style="border:1px solid #ccc; padding:3px;"/>
</div>

3. **Refine Tracking**:
   - If needed, clean up bad tracks by selecting them (**Ctrl + L**) and deleting failed markers.

<div align="center">
<img src="https://github.com/user-attachments/assets/ebc48207-6d8c-484d-bf25-4e8bfcadd463" width="40%" style="border:1px solid #ccc; padding:3px;"/>
</div>

---

### Step 5: Set Up the 3D Scene
1. **Set Up Scene**:
   - Click **Set Up Tracking Scene** in the Solve tab. Blender will add a camera and a basic plane.
2. **Adjust Scale**:
   - Go to the 3D viewport and adjust the scene scale by aligning 3D objects with tracked markers.

---

### Step 6: Render the Scene
1. **Enable Background Video**:
   - In **Render Properties**, enable the background video to appear during rendering.
2. **Play the Animation**:

<div align="center">
<img src="https://github.com/user-attachments/assets/9b1833ae-3fb8-45a1-afe5-543d10c3f9ff" width="70%" style="border:1px solid #ccc; padding:3px;"/>
</div>

---

### Step 7: Improve Tracking (If Needed)
- If motion tracking is not accurate:
  - Go back to **Detect Features**.
  - Use a **closer threshold and distance** to detect more reliable markers.
  - Repeat **Steps 3–6** to refine the tracking and improve accuracy.

---

## Tips & Best Practices
- Use footage with **good lighting and high-contrast features**.
- Ensure at least **8–10 tracking points** for accurate camera solving.
- Clean up any **drifting or failed markers** before solving camera motion.
