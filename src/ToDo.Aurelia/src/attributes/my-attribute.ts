import {autoinject} from 'aurelia-framework';

@autoinject()
export class MyAttributeCustomAttribute {
  constructor(private element: Element) { }

  valueChanged(newValue, oldValue) {
    //
  }
}
