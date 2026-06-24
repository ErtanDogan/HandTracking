# 👆 Hand-Tracking Computer Vision Project 👆

> *This project may be expanded soon*

## 🌟 Highlights

- A fine-tuned YOLO model that classifies your real-life hand in real time using your computer webcam
- This is integrated into a Unity simulation, affecting objects in the simulation based on real-life hand movements

## ℹ️ Overview

This project is an experiment with real-time computer vision and integrating that into a simulation environment. 

The model is a fine-tuned YOLO-11 Nano, a powerful and compact computer vision model by Ultralytics built for object detection, classification, and image segmentation. It is fine-tuned to recognize a human hand in three "positions" - in a fist, in an open palm, and pointing.

The program uses the OpenCV library to read from the user's computer webcam in real time which is then processed by the YOLO model to recognize the hand shape. The classified image is broadcasted using a Flask server running on the local machine.

The simulation is created and run inside Unity. It is currently very simple, only containing a hand model that can be a fist, an open palm, or pointing. It reads data from the Flask server using Unity's Networking framework and updates the in-simulation hand accordingly in real time.

## 🚀 Usage

The github repository has all of the files necessary for the unity project, as well as two (best.pt and YOLO_Unity.py) that are for the model and the python script that powers it. The Unity files should be imported and assembled into a Unity3D project, but no additional packages need to be downloaded. The python file and the model weights should be separate.

To run the program, ensure that `best.pt` and `YOLO_Unity.py` are in the same directory, and that you have these python libraries:
- Ultralytics
- OpenCV for Python
- Flask and Flask CORS
- Pytorch (torch, torchvision)
- Numpy
- Matplotlib.pyplot for visualizing the images
And you will need the Unity Game Engine

While running the `YOLO_Unity.py` script, your device's webcam will not be able to be used by any other application, but when you deactivate the Flask server, the camera will be restored.

After the server is running (if it is not on port 5000 of localhost, the Unity script will have to be edited slightly), the project in Unity can be run and will begin simulation. Make sure to monitor the Unity debug console for any errors in reading the server.

## 💭 Limitations and Next Steps

The model is trained exclusively using images of my real-life right hand. This may lead to bias when classifying other people's hands, and possibly also in classifying left hands (but the simulation is a right hand anyways so this is technically a feature not a bug).

For how I plan on expanding the project, my immediate idea is to further train the YOLO model to more accurately track the hand's position, and reflect this in the Unity simulation. Then, I think it'd be cool to add interactive objects in the simulation that the user will be able to pick up, move, and drop using their real-life hand. 

### ✍️ Authors

I'm Ertan Dogan, a computer engineering student interested in machine learning and its integration into simulation and robotics.
