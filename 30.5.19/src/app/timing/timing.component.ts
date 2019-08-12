import { Component, OnInit, Input, Output, EventEmitter, AfterViewInit} from '@angular/core';
import { FrequencyEnum, WeekDayEnum, RequestDto, TimingClass } from '../models';

@Component({
  selector: 'app-timing',
  templateUrl: './timing.component.html',
  styleUrls: ['./timing.component.css']
})
export class TimingComponent implements OnInit, AfterViewInit {
 
  frequency = FrequencyEnum
  days=WeekDayEnum
  keys: any[];
  hours =[];
  frequencyIdChoose;

@Input('requestTiming') timing:TimingClass;
  @Output()
  selectTiming=  new EventEmitter();

 onSelectTiming() {       
        this.selectTiming.emit(this.timing)
    }

  constructor() {
    this.keys = Object.keys(this.frequency).filter(Number);
    this.keys = Object.keys(this.days).filter(Number)
    this.timing=new TimingClass();
    for(var i = 0; i < 11; ++i) {
        var hour = i < 10 ? '0' + i : i;
         this.hours[i] = hour + ':00AM';
    }
    this.hours[11] = '11:00AM';
    this.hours[12] = '12:00AM';
  
    for(var i = 0; i < 11; ++i) {
        var hour = i < 10 ? '0' + i : i;
         this.hours[i + 12 ] = hour + ':00PM';
    }
    this.hours[23] = '12:00PM';
   console.log(this.hours)
   }
   ChangeFrequency(frequency){
    this.frequencyIdChoose=frequency; 
  }

  ngOnInit() {
  
  }

  ngAfterViewInit() {
    this.ChangeFrequency(this.timing.frequency_id); 

  }
}



