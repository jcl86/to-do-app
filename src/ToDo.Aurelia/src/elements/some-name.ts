import {bindable} from 'aurelia-framework';

export class SomeName {
  @bindable value;

  valueChanged(newValue, oldValue) {
    //
  }
}
