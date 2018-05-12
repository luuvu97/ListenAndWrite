function CompactString() {
    this.i_ll = "i will",
    this.he_ll =  "he will",
    this.she_ll = "she will",
    this.we_ll = "we will",
    this.they_ll = "he will",
    this.didn_t = "did not",
    this.don_t = "do not",
    this.doesn_t = "does not",
    this.aren_t = "are not",
    this.can_t = "can not",
    this.couldn_t = "could not",
    this.hasn_t = "has not",
    this.hadn_t = "had not",
    this.haven_t = "have not",
    this.mustn_t = "must not",
    this.shan_t = "shall not",
    this.shouldn_t = "should not",
    this.wasn_t = "was not",
    this.won_t = "will not",
    this.wouldn_t = "would not",
    this.mightn_t = "might not",
    this.mayn_t = "may not",
    this.i_m = "i am",
    this.there_re = "there are",
    this.they_re = "they are",
    this.we_re = "we are",
    this.you_re = "you are",
    this.i_ve = "i have",
    this.we_ve = "we have"
}

CompactString.prototype.parseWord = function(inp){
    //parse an input string and return an array of string
    //ex: inp: i'm => out ["i'm", "i am"]
    //inp: hello => out ["hello"]
    var ret = new Array();
    ret.push(inp);
    inp = inp.replace("'", "_");
    if(this.hasOwnProperty(inp)){
        //console.log("contain");
        ret.push(this[inp].replace("_", "'"));
    }
    return ret;
}

CompactString.prototype.getCompactWord = function (inp) {
    //return the word in compact mode if the word can be write in compact type
    for (var e in this) {
        if (this[e] == inp) {
            return e.replace("_", "'");
        }
    }
    return inp;
}

//input a string then the output is another string be compact from input string
CompactString.prototype.compactString = function (inp) {
    var outStr = "";
    inp = inp.toLowerCase();
    return this.cs1(inp.split(" "), 0, outStr);
}

CompactString.prototype.cs1 = function(inpStr, index, outStr){
    //inpStr array of string: input string after split
    //index: current index
    var str = "";
    var offset = 1;
    if(index < inpStr.length - 1){
        str = inpStr[index] + " " + inpStr[index + 1];
    }else if(index == inpStr.length - 1){
        str = inpStr[index];
    }

    var tmp = this.getCompactWord(str);
    if (tmp.indexOf("'") != -1) {
        //xau co the viet tat
        offset = 2;
        outStr += tmp;
    } else {
        outStr += inpStr[index];
    }

    if ((index + offset) < inpStr.length) {//not end
        outStr += " ";
        return this.cs1(inpStr, index + offset, outStr);
    } else {
        return outStr;
    }
}


//CompactString.prototype.compactString1 = function(inp){
//    var script = inp;
//    var tmp;
//    while(true){
//        tmp = script;
//        for(var e in this){
//            if(this.hasOwnProperty(e)){
//                script = script.replace(this[e], e.replace("_", "'"));
//                //if don't have " " (space) in front and rear => "care not" be replaced by "are not" and return wrong answer "caren't"
//            }
//        }
//        if(tmp == script){
//            break;
//        }
//    }
//    //console.log("Compact string: inp -- out: " + script);
//    return script;
//}

//parese a string into an array of string
//ex "i am vu" => compact string "i'm vu" => array of string [ ["i'm", "i am"], ["vu"]];
CompactString.prototype.parseScript = function(inp){
    inp = inp.toLowerCase();
    inp = this.compactString(inp);
    inp = inp.split(" ");
    var ret = new Array();
    for(var key in inp){
        ret.push(this.parseWord(inp[key]));
    }
    return ret;
}