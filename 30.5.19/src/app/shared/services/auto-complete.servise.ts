import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';

import { BaseApiService, BaseHttpService } from "../../shared";
import { PersonSmall } from "../models";
import { personTypeEnum } from "../../models";
import { Input, Component } from "@angular/core";

@Injectable()
export class AutoCompleteService extends BaseApiService{
     
    constructor(private baseHttpService: BaseHttpService){
        super('Persons');
    }

    getPeople(lettersContained, personType, contains){
        let url; 
        let params: URLSearchParams = new URLSearchParams();
        if(contains){
            switch (+personType) {
                case personTypeEnum.femaleCandidate:
                {
                    url = this.actionUrl('GetNamesOfCandidateByGenderContain');
                    params.set('genderCode',personType);
                    break;
                }     
                case personTypeEnum.maleCandidate:
                {
                    url = this.actionUrl('GetNamesOfCandidateByGenderContain');
                    params.set('genderCode',personType);
                    break;
                }
                default:
                {
                    url = this.actionUrl('GetAllNamePersons');
                    break;
                }
            }
            params.set('contain',lettersContained);
        }else {
            switch (+personType) {
                case personTypeEnum.femaleCandidate:
                {
                    url = this.actionUrl('GetNamesOfCandidateByGenderStartWith');
                    params.set('genderCode',personType);
                    break;
                }     
                case personTypeEnum.maleCandidate:
                {
                    url = this.actionUrl('GetNamesOfCandidateByGenderStartWith');
                    params.set('genderCode',personType);
                    break;
                }
                default:
                {
                    url = this.actionUrl('GetAllNamePersons');
                    break;
                }
            }
            params.set('startWith',lettersContained);
        }
        return this.baseHttpService.get<PersonSmall[]>(url, params)
    }
    
}