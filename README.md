# Pixel_Hunter2.0
Upgraded version of [Pixel Hunter](https://github.com/NorbertKominak/Pixel_Hunter1.0) with graphical interface, pararell execution of neural nets inference and async API communication. Outputs are in .csv format and if chosen .jpeg(bounding boxes with labels) as well.

This desktop app is developed using WinForms and is compatible with Windows only. 

## Installation
1. Download pre-trained models with python scripts and all dependencies from [here](https://drive.google.com/file/d/1WBIn7GBaynfLTLeM4yz8sbd0MFc_QngR/view?usp=sharing)
2. Unzip models2.zip into ./PixelHunter folder.
3. In order to run this app you need to open this solution in Visual Studio and compile it.

## Usage
1. Specify input and output directory. Input represents directory with images and output states where results will be stored.
2. Tick which freely available models should be used for the analysis.
3. If required tick which APIs should be run on images evaluated as problematic for the freely available models. API`s credentials prompted when is ticked.
4. Tick visual output option if additional visual outputs (original images with bounding boxes, scores and labels) are desirable. Works only for Age and Gender Estimation and Object Detection models.
5. Click Run Analysis. Analysis can be cancelled at any time. When finished, you can find results in the specified output directory. 
![Example](https://github.com/NorbertKominak/Pixel_Hunter2.0/blob/main/demo.png) 

## Sources
[Bachelor Thesis](https://is.muni.cz/th/o7q0i/?lang=en)  
[NSFW model](https://github.com/GantMan/nsfw_model)  
[Age_gender model](https://github.com/yu4u/age-gender-estimation)  
[Scene description model](https://github.com/CSAILVision/places365)  
[Object detection model](https://github.com/tensorflow/models/blob/master/research/object_detection/g3doc/tf2_detection_zoo.md)  

## License
[MIT](https://choosealicense.com/licenses/mit/)

