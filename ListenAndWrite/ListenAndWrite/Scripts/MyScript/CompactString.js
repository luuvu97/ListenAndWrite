function CompactString(){
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
    this.i_m = "i am"
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

CompactString.prototype.compactString = function(inp){
    var script = inp;
    var tmp;
    while(true){
        tmp = script;
        for(var e in this){
            if(this.hasOwnProperty(e)){
                script = script.replace(this[e], e.replace("_", "'"));
            }
        }
        if(tmp == script){
            break;
        }
    }
    //console.log("Compact string: inp -- out: " + script);
    return script;
}

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