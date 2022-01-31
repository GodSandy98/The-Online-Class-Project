# -*- coding: utf-8 -*-
"""
Created on Sat Feb  6 15:22:03 2021

@author: Zerui Mu

Face detection test
"""

import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
import cv2
import os, sys, time

def readImage(imagePath):
    '''
    Read the image by path.
    
    Parameters
    ----------
    imagePath : String
        Path of the picture to be detected.

    Returns
    -------
    img : dst
        Image.

    '''
    if os.path.exists(imagePath):
        img_read = cv2.imread(imagePath)
        img = cv2.cvtColor(img_read, cv2.COLOR_BGR2RGB)
        return img
    return False

def showImage(image):
    '''
    Show the image.

    Parameters
    ----------
    image : dst
        Image.

    Returns
    -------
    None.

    '''
    plt.axis('off')
    plt.imshow(image)
    plt.show()
    
def openLocalCamera(cameraIndex):
    '''
    Open specific camera.

    Parameters
    ----------
    cameraIndex : int
        Local camera index.

    Returns
    -------
    capture : VideoCapture
        Current video device.

    '''
    capture = cv2.VideoCapture(cameraIndex)
    return capture 
    
def detectFaces(image):
    '''
    Detect faces from image and add green rectangle on original image.

    Parameters
    ----------
    image : dst
        The image to be detected.

    Returns
    -------
    faces_num : int
        The number of faces in the image.

    '''
    gray = cv2.cvtColor(image, cv2.COLOR_RGB2GRAY)
    classifierPath = '../../../screenshot/haarcascade_frontalface_default.xml'
    detector = cv2.CascadeClassifier(classifierPath)
    faces = detector.detectMultiScale(gray, scaleFactor=1.1, minNeighbors=2, minSize=(10,10), flags = cv2.CASCADE_SCALE_IMAGE)
    print_color = (0, 255, 0)
    faces_num = len(faces)
    for (x,y,w,h) in faces:
        cv2.rectangle(image, (x,y), (x+w,y+h), print_color, 2)
    return faces_num

def runSeveralTimes(times, timeSpan, cameraIndex):
    '''
    Detect for several times and return the number of faces.

    Parameters
    ----------
    times : int
        Loop times.
    timeSpan : double
        Time interval between each loop.
    cameraIndex : int
        Local camera index.

    Returns
    -------
    result : list
        Face number of each detection.

    '''
    result = []    
    capture = openLocalCamera(cameraIndex)
    for i in range(times):
        ret, image = capture.read()
        result.append(detectFaces(image))
        showImage(image)
        time.sleep(timeSpan)
    return result

def runOneTime(cameraIndex):
    '''
    Detect one time and return whether there was at least one face.

    Parameters
    ----------
    cameraIndex : int
        Local camera index.

    Returns
    -------
    result : int
        The number of faces in the detection.

    '''
    capture = openLocalCamera(cameraIndex)
    ret, image = capture.read()
    # showImage(image)
    result = detectFaces(image)
    return result

def runByFile(imagePath):
    '''
    Return the number of faces in the image.

    Parameters
    ----------
    imagePath : String
        Path of the picture to be detected.

    Returns
    -------
    result : int
        The number of faces in the image.
        (-1 means imagePath doesnt exist)

    '''
    image = readImage(imagePath)
    if type(image) == bool:
        return -1
    result = detectFaces(image)
    return result

def printResult(result):
    '''
    Transform the number of faces or error code into result and print to console.

    Parameters
    ----------
    result : int
        The number of faces or error code.

    Returns
    -------
    None.

    '''
    if result == -1:
        print("File doesn't exist")
    elif result == 0:
        print('False')
    else:
        print('True')
    

if __name__ == '__main__':
    # imagePath = '../../Pic/ID.jpg'
    # imagePath = '../../Pic/scientists.jpeg'
    imagePath = '../../../screenshot/localCapture.jpg'
    
    # result = runOneTime(cameraIndex = 0)
    # result = runSeveralTimes(times = 5, timeSpan = 1, cameraIndex = 0)
    result = runByFile(imagePath)
    printResult(result)
    print("Finish!")
    
    cv2.destroyAllWindows()
    

