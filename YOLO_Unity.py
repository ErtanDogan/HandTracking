from flask import Flask, Response
from flask_cors import CORS

import cv2
import numpy as np
import matplotlib.pyplot as plt
import torch, torchvision

from ultralytics import YOLO

cap = cv2.VideoCapture(0)

ret, frame = cap.read()
if ret:
    print("Camera is working.")
    
model = YOLO("runs/detect/train-17/weights/best.pt")

app = Flask(__name__)

@app.route("/")
def return_data():
    def predict():
        ret, frame = cap.read()
    
        if not ret:
            return 0
    
        results = model(frame)
        if results[0].boxes.cls.shape[0] == 0:
            num_out = 0
            return "0 0.5 0.5"
        else:
            num_out = int(results[0].boxes.cls[0].item())
            x, y, w, h = results[0].boxes.xywh[0]
            return f"{num_out} {x/640} {y/480}"
    return Response(predict(), mimetype='text/plain')

app.run()
cap.release()