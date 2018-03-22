function ChooseDiv(btnComplete, btnChooseNext, btnChooseListenAgain) {
    this.btnComplete = document.getElementById(btnComplete);
    this.btnChooseNext = document.getElementById(btnChooseNext);
    this.btnChooseListenAgain = document.getElementById(btnChooseListenAgain);
}

ChooseDiv.prototype.hide = function () {
    this.btnComplete.style.display = "none";
    this.btnChooseListenAgain.style.display = "none";
    this.btnChooseNext.style.display = "none";
}

ChooseDiv.prototype.show = function (isEnd) {
    debugger
    if (isEnd == true) {
        this.btnComplete.style.display = "Block";
        this.btnChooseListenAgain.style.display = "Block";
    } else {
        this.btnChooseListenAgain.style.display = "Block";
        this.btnChooseNext.style.display = "Block";
    }
}