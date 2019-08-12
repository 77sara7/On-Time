import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { HttpClientModule } from "@angular/common/http";

import { SharedServicesModule} from "./services";
import { UixModule } from "./uix";

const EXPORTED_MODULES = [
	
	SharedServicesModule,
	UixModule
];

@NgModule({
	imports: [
		BrowserModule,
		HttpClientModule,
	],
	exports: [
		...EXPORTED_MODULES
	],
})
export class SharedModule { }