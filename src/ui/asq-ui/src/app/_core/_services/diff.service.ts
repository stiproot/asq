import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DiffService {

  private verboseLogging = false;
  private metadata = ['creationDateUtc', 'creationUserId', 'inactive', 'id', 'focus', 'uniqueId', 'passwordConfirm'];

  constructor() { }

  public isDiff(newObj: object, oldObj: object, key: string = null): boolean{
      // let's keep the standard that newObj should be the newer obj

      // safety checks
      if (newObj === null && oldObj === null){
        if (this.verboseLogging){
          console.log('DiffService.isDiff', 'both are null');
        }
        return false;
      }
      if ((newObj !== null && oldObj === null)){
        if (this.verboseLogging){
          console.log('DiffService.isDiff', 'oldObj is null');
        }
        return true;
      }
      if ((newObj === null && oldObj !== null)){
        if (this.verboseLogging){
          console.log('DiffService.isDiff', 'newObj is null');
        }
        return true;
      }
      if (newObj === undefined && oldObj === undefined){
        if (this.verboseLogging){
          console.log('DiffService.isDiff', 'both are undefined');
        }
        return false;
      }
      if (newObj === undefined && oldObj !== undefined){
        if (this.verboseLogging){
          console.log('DiffService.isDiff', 'newObj is undefined');
        }
        return true;
      }
      if (newObj !== undefined && oldObj === undefined){
        if (this.verboseLogging){
          console.log('DiffService.isDiff', 'oldObj is undefined');
        }
        return true;
      }

      const newObjProps = Object.getOwnPropertyNames(newObj).filter(x => this.metadata.indexOf(x) === -1);
      const oldObjProps = Object.getOwnPropertyNames(oldObj).filter(x => this.metadata.indexOf(x) === -1);

      if (newObjProps.length !== oldObjProps.length){
        if (this.verboseLogging){
          console.log('DiffService.isDiff', 'lengths not equal');
        }
        return true;
      }
      let diff = false;
      newObjProps.forEach((element, index) => {
        const newObjEl = newObj[element];
        const oldObjEl = oldObj[element];

        // if (this.verboseLogging){
          // console.log(element, 'isDiff', 'newObj-element', newObjEl, '|', 'oldObj-element', oldObjEl, '|', 'typeof element', typeof(newObjEl));
        // }

        // type check
        if (typeof newObjEl !== typeof oldObjEl){
          diff = true;
        }

        if (typeof newObjEl !== 'object'){
          if (newObjEl !== oldObjEl){
            if (!diff){
              diff = true;
              if (this.verboseLogging){
                console.log(element, 'isDiff', 'newObj-element', newObjEl, '|', 'oldObj-element', oldObjEl, '|', 'typeof element', typeof(newObjEl));
              }
            }
            // console.log('isDiff', 'newObj-element', newObjEl, '|', 'oldObj-element', oldObjEl);
            // return true;
          }
        }
        else if (typeof newObjEl === 'object' && Array.isArray(newObjEl)){
          // handle arrays
          if(newObjEl.length !== oldObjEl.length){
            diff = true;
          }
          else{
            newObjEl.forEach((_element, _index) => {
              let _diff = this.isDiff(_element, oldObjEl[_index]);
              if (!diff && _diff){
                diff = _diff;
              }
            });
          }
        }
        else{
          // handle object
          const _diff = this.isDiff(newObjEl, oldObjEl, element);
          if (!diff && _diff){
            diff = _diff;
          }
        }
      });
      return diff;
  }
}
