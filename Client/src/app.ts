import { HttpClient, json } from 'aurelia-fetch-client';
import { autoinject } from 'aurelia-framework';
import environment from './environment';

@autoinject()
export class App {

  uploadedFiles: FileList = null;
  uploadedId : string;

  constructor(private client: HttpClient) {
    this.client.configure(config => {
      config.useStandardConfiguration();
    })
  }

  attached() {
    // return this.client.fetch(`${environment.api}/passwords`)
    //   .then(response => response.json<string[]>())
    //   .then(x => alert(x))
    //   .catch(rej => alert(rej));     

  }

  upload() {
    var formData = new FormData();
    for (let i = 0; i < this.uploadedFiles.length; i++) {
      formData.append('uploadedFiles', this.uploadedFiles[0])
    }

    return this.client.fetch(`${environment.api}/passwords/upload`, {
      method: 'post',
      body: formData
    }).then(x => x.json<string[]>())
      .then(x => this.uploadedId[0])
      .catch(rej => alert(rej));      
  }

  process(){
     return this.client.fetch(`${environment.api}/password/process/${this.uploadedId}`, {
       method: 'get',       
     })
     .then(x => x.json<any>())
     .then(x => alert(x));
  }



  message = 'Hello World!';
}
