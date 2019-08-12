import { Component, Input, Output, EventEmitter } from "@angular/core"
import {AutoCompleteModule} from 'primeng/autocomplete';
import { element } from "protractor";
import { AutoCompleteService } from "../../../services";
import { PersonSmall } from "../../../models";

@Component({
    templateUrl: "./auto-complete.component.html",
    selector: "m-auto-complete"
})

export class AutoCompleteComponent{

    @Input()
    personType: number;

    @Output()
    value =  new EventEmitter<PersonSmall>();;

    @Input()
    contains:boolean = false;
    @Input()
    placeHolder:string="";
    constructor(private autoCompleteService: AutoCompleteService) { }
    val:PersonSmall;
    results: PersonSmall[] = [];
    
    search(event) {
    //    let query = event.query;
    //    this.autoCompleteService.getPeople(query,this.personType,this.contains)
    //    .subscribe(
    //         (data: PersonSmall[]) => {
    //            this.results = data;
    //         });   
    //     }
    //     selected(){
    //         this.value.emit(this.val);
    //     }
    }}

    
    
   