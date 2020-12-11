import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class UploadImageService {

  constructor(private http:HttpClient) { }
  readonly BaseURI = 'https://localhost:44361/api';

  postFile(caption: string, fileToUpload: File) {
    const endpoint = this.BaseURI + '/UploadImage';
    const formData: FormData = new FormData();
    formData.append('Image', fileToUpload);
    formData.append('Name', fileToUpload.name);
    formData.append('ImageCaption', caption);
    return this.http
      .post(endpoint, formData, {responseType: 'text'});
  }

}
