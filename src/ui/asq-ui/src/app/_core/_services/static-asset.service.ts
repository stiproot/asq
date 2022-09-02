import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StaticAssetService {

  private get _baseUrl(): string{
    return environment.staticAssetBaseUrl;
  }

  constructor() { }

  public staticUrl(assetId: string): string | null{
    return `${this._baseUrl}${assetId}`;
  }
}
