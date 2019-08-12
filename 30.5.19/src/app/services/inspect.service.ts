import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import { EventEmitter } from 'protractor';
import { browser } from 'protractor';

@Injectable()
export class InspectService{

    my() {
        //      var targetSelecter = new TargetSelecter(
        //          function (element, win) {
        //         if (element && win) {
        //             var locatorBuilders = new locatorBuilders(win);
        //             var target = locatorBuilders.buildAll(element);
        //             locatorBuilders.detach();
        //             if (target != null && target instanceof Array) {
        //                 if (target) {
        //                     //self.editor.treeView.updateCurrentCommand('targetCandidates', target);
        //                     browser.runtime.sendMessage({
        //                         selectTarget: true,
        //                         target: target
        //                     })
        //                 } else {
        //                     //alert("LOCATOR_DETECTION_FAILED");
        //                 }
        //             }
    
        //         }
        //          targetSelecter = null;
        //     }, 
        //     function () {
        //         browser.runtime.sendMessage({
        //             cancelSelectTarget: true
        //         })
        //     }
        // );
        }

onInit(){
    //לשים את כל  הפונקציות בחוץ ורק לקרוא להן הOמINוןא
    document.body.addEventListener("click", event => {         
       
        console.log(event.target);
    })

    
    var onFileSelect = function (file) {
        var reader = new FileReader();
        reader.onload = function (e) {
            console.log("about to encode");
            // var encoded_file = btoa(e.target.result.toString());
        };
        reader.readAsBinaryString(file);
    };


    //click event
    // this.my();


    // Modified in tools.js from selenium-IDE
    // function 
    
    function TargetSelecter(callback, cleanupCallback) {
        this.callback = callback;
        this.cleanupCallback = cleanupCallback;

        // This is for XPCOM/XUL addon and can't be used
        //var wm = Components.classes["@mozilla.org/appshell/window-mediator;1"].getService(Components.interfaces.nsIWindowMediator);
        //this.win = wm.getMostRecentWindow('navigator:browser').getBrowser().contentWindow;

        // Instead, we simply assign global content window to this.win
        this.win = window;
        var doc = this.win.document;
        var div = doc.createElement("div");
        div.setAttribute("style", "display: none;");
        doc.body.insertBefore(div, doc.body.firstChild);
        this.div = div;
        this.e = null;
        this.r = null;
        doc.addEventListener("mousemove", this, true);
        // doc.addEventListener("click", this, true);
    }

    TargetSelecter.prototype.cleanup = function () {
        try {
            if (this.div) {
                if (this.div.parentNode) {
                    this.div.parentNode.removeChild(this.div);
                }
                this.div = null;
            }
            if (this.win) {
                var doc = this.win.document;
                doc.removeEventListener("mousemove", this, true);
                // doc.removeEventListener("click", this, true);
            }
        } catch (e) {
            if (e != "TypeError: can't access dead object") {
                throw e;
            }
        }
        this.win = null;
        if (this.cleanupCallback) {
            this.cleanupCallback();
        }
    };

    TargetSelecter.prototype.handleEvent = function (evt) {
        debugger
        switch (evt.type) {
            case "mousemove":
                this.highlight(evt.target.ownerDocument, evt.clientX, evt.clientY);
                break;
            //case "click":
            //    if (evt.button == 0 && this.e && this.callback) {
            //        this.callback(this.e, this.win);
            //    } //Right click would cancel the select;
            //  console.log(evt.toElement);
            //    evt.preventDefault();
            //    evt.stopPropagation();
            //    this.cleanup();
            //    break;
        }
    };

    TargetSelecter.prototype.highlight = function (doc, x, y) {
        if (doc) {
            var e = doc.elementFromPoint(x, y);
            if (e && e != this.e) {
                this.highlightElement(e);
            }
        }
    }

    TargetSelecter.prototype.highlightElement = function (element) {
        if (element && element != this.e) {
            this.e = element;
        } else {
            return;
        }
        var r = element.getBoundingClientRect();
        var or = this.r;
        if (r.left >= 0 && r.top >= 0 && r.width > 0 && r.height > 0) {
            if (or && r.top == or.top && r.left == or.left && r.width == or.width && r.height == or.height) {
                return;
            }
            this.r = r;
            var style = "pointer-events: none; position: absolute; box-shadow: 0 0 0 1px black; outline: 1px dashed white; outline-offset: -1px; background-color: rgba(250,250,128,0.4); z-index: 100;";
            var pos = "top:" + (r.top + this.win.scrollY) + "px; left:" + (r.left + this.win.scrollX) + "px; width:" + r.width + "px; height:" + r.height + "px;";
            this.div.setAttribute("style", style + pos);
        } else if (or) {
            this.div.setAttribute("style", "display: none;");
        }
    };


    function hoverFunc(element) {
        var myDiv = document.createElement("div")
        myDiv.setAttribute("id", "myDiv")
        var r = element.getBoundingClientRect();
        this.win = window;
        var style = "pointer-events: none; position: absolute; box-shadow: 0 0 0 1px black; outline: 1px dashed white; outline-offset: -1px; background-color: rgba(250,250,128,0.4); z-index: 100;";
        var pos = "top:" + (r.top + this.win.scrollY) + "px; left:" + (r.left + this.win.scrollX) + "px; width:" + r.width + "px; height:" + r.height + "px;";
        myDiv.setAttribute("style", style + pos);
        document.getElementsByTagName("body")[0].appendChild(myDiv)
    }
    function unHoverFunc(element) {
        document.getElementById("myDiv").remove()

    }

}}
  