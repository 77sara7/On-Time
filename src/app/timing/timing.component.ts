import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FrequencyEnum, WeekDayEnum, RequestDto } from '../models';
import { GlobalService } from '../shared';

@Component({
  selector: 'app-timing',
  templateUrl: './timing.component.html',
  styleUrls: ['./timing.component.css']
})
export class TimingComponent implements OnInit {

  frequency = FrequencyEnum;
  days = WeekDayEnum;
  hours = [];
  frequencyIdChoose;
  frequencyLabel = [];
  day = [];
  stringValueFrequency: string;
  stringValueHour: string;
  stringValueDay: string;

  @Input()
  fillRequest: RequestDto = new RequestDto();

  @Output()
  onRequestChanged: EventEmitter<RequestDto> = new EventEmitter<RequestDto>();

  constructor(private globalService: GlobalService) {
    
  }
  ngOnInit() {
    this.fillArrays();
    this.stringValueFrequency = FrequencyEnum[this.fillRequest.frequency_id];
    if (this.fillRequest.hour) {
      this.stringValueHour = this.hours[this.fillRequest.hour - 1].value;
    }
    if (this.fillRequest.day) {
      this.stringValueDay = this.days[this.fillRequest.day];
    }
  }
  ChangeFrequency(frequency) {

    this.frequencyIdChoose = frequency.value;
    this.fillRequest.frequency_id = parseInt(FrequencyEnum[frequency.value]);
    this.onChangeRequest(frequency);
  }

  onChangeRequest(event) {
    if (this.fillRequest.frequency_id == FrequencyEnum.day) {
      this.fillRequest.hour = this.hours.findIndex(h => h.value === event.value) + 1;
    }
    else if (this.fillRequest.frequency_id == FrequencyEnum.week) {
      this.fillRequest.day = parseInt(WeekDayEnum[event.value]) + 1;
    }

    this.onRequestChanged.emit(this.fillRequest);
  }

  fillArrays() {
    for (let i = 0; i < 7; i++) {
      this.day.push({ label: this.days[i], value: this.days[i] });
    }
    for (let i = 0; i < 5; i++) {
      this.frequencyLabel.push({ label: this.frequency[i], value: this.frequency[i] });

    }
    for (let i = 1; i < 11; i++) {
      var hour = i < 10 ? '0' + i : i;
      this.hours.push({ label: hour + ':00AM', value: hour + ':00AM' });
    }
    this.hours.push({ label: '11:00AM', value: '11:00AM' });
    this.hours.push({ label: '12:00AM', value: '12:00AM' });
    for (let i = 1; i < 11; i++) {
      var hour = i < 10 ? '0' + i : i;
      this.hours.push({ label: hour + ':00PM', value: hour + ':00PM' });

    }
    this.hours.push({ label: '11:00PM', value: '11:00PM' });
    this.hours.push({ label: '12:00PM', value: '12:00PM' });
  }

}



