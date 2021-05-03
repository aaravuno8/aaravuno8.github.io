const video = document.getElementById("video")

models_url = '../Face/models'

Promise.all([
  faceapi.nets.tinyFaceDetector.loadFromUri(models_url),
  faceapi.nets.faceLandmark68Net.loadFromUri(models_url),
  faceapi.nets.faceRecognitionNet.loadFromUri(models_url),
  faceapi.nets.faceExpressionNet.loadFromUri(models_url),
]).then(startVideo());

function startVideo() {
  navigator.getUserMedia(
    { video: {} },
    (stream) => (video.srcObject = stream),
    (err) => console.error(err)
  );
}

var isMobile = /iPhone|iPad|iPod|Android/i.test(navigator.userAgent);
if (isMobile) {
  document.querySelector("#mobile").style.display = "block";
} else {
  document.querySelector("#mobile").style.display = "none";
}

video.addEventListener("play", () => {
  const canvas = faceapi.createCanvasFromMedia(video);
  document.body.append(canvas);
  const displaySize = { width: video.width, height: video.height };
  faceapi.matchDimensions(canvas, displaySize);
  setInterval(async () => {
    if (document.querySelector("#flexSwitchCheckChecked").checked) {
      document.querySelector("#video").style.visibility = "unset";
      const detections = await faceapi
        .detectAllFaces(video, new faceapi.TinyFaceDetectorOptions())
        .withFaceLandmarks()
        .withFaceExpressions();
      const resizedDetections = faceapi.resizeResults(detections, displaySize);
      console.log(resizedDetections);
      canvas.getContext("2d").clearRect(0, 0, canvas.width, canvas.height);
      if (document.querySelector("#face-check").checked) {
        faceapi.draw.drawDetections(canvas, resizedDetections);
      }
      if (document.querySelector("#landmarks-check").checked) {
        faceapi.draw.drawFaceLandmarks(canvas, resizedDetections);
      }
      if (document.querySelector("#emo-check").checked) {
        faceapi.draw.drawFaceExpressions(canvas, resizedDetections);
      }
    } else {
      document.querySelector("#video").style.visibility = "hidden";
      canvas.getContext("2d").clearRect(0, 0, canvas.width, canvas.height);
    }
  }, 100);
});

