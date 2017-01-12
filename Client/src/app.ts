import {HttpClient} from 'aurelia-fetch-client';
import {autoinject} from 'aurelia-framework';
import environment from './environment'; 

@autoinject()
export class App {

  constructor(private client : HttpClient){
     
  }

  attached() {
    return this.client.fetch(`${environment.api}/passwords`)
      .then(response => response.json<string[]>())
      .then(x => alert(x))
      .catch(rej => alert(rej));     
     
  }

  message = 'Hello World!';
}
