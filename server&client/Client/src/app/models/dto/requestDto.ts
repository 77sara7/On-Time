import { Guid } from 'guid-typescript';

export class RequestDto {
    request_id: number;
    request_name: string;
    date_from = new Date();
    date_to = new Date();
    user_id: number;
    day: number;
    hour: number;
    frequency_id: number;
    file_stream: number[];
    recording_stream: number[];
    file_name: string;
    file_id;
    recording_id: Guid;
    content: string;
    is_relevant: Boolean;
    constructor() {
        this.is_relevant = true;
    }

}
