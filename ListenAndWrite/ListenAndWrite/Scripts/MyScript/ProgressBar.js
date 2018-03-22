function ProgressBar(progressBar, progressText) {
    this.progressBar = document.getElementById(progressBar);
    this.progressText = document.getElementById(progressText);
}

ProgressBar.prototype.showProgress = function (curPoint, maxPoint) {
    this.show();
    this.progressBar.style.width = 100 * (curPoint / maxPoint) + "%";
    this.progressText.innerText = curPoint + " / " + maxPoint;
}

ProgressBar.prototype.show = function () {
    this.progressBar.style.display = "block";
    this.progressText.style.display = "block";
}

ProgressBar.prototype.hide = function () {
    this.progressBar.style.display = "None";
    this.progressText.style.display = "None";
}