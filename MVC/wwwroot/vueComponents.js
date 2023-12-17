var compBase = {
  props: {
    'text': String,
    'req': Boolean,
    'requretext': {type:String, default:'Значение должно быть заполнено!!!'}
  },
  data: function () {
    return {
      valueInt: null,
      valid: true,
      
      notValidText: null
    }
  },
  watch: {
    immediate: true,
    value(val) {
       this.valueInt = val;
    }
  },
  methods: {
    //virtual method 
    virtChange(val){
      this.valueInt = val
      this.$emit('input', val)//event to parent
    }
   ,
    valChanged(event) {
      this.virtChange( event.target.value)
      this.Validate(event.target.value)
    },

    Validate(val) {

      if (this.req && !val) {
        this.valid = false
        this.notValidText = this.requretext;
      } else {
        this.valid = true
        this.notValidText = null;
      }
    },
  },
  mounted: function () {
    this.valueInt = this.value
  }
}

Vue.component('kf-input', {
  mixins: [compBase],
  props: {
    'value': { type: String },
  },

  methods: {
  },
  template: `
   <div class="flex-row" >
   <div class="title-col"  >{{ text }}:</div>
   
   <input 
    class="value-col inp " :class="{ invalid: !valid }" 
    :title="notValidText" 
     v-bind:value="value"
     v-on:input="valChanged($event)"
   />

   </div>
 `
})

Vue.component('kf-date', {
  mixins: [compBase],
  props: {
    'value': { type: [String,Date] },
  },

  methods: {
  },
  template: `
   <div class="flex-row" >
   <h3 class="title-col" >{{ text }}:</h3>
 
   <input type='date'
    class="value-col inp " :class="{ invalid: !valid }" 
    :title="notValidText" 
     v-bind:value="value"
     v-on:input="valChanged($event)"
   />
   <img v-if="!valid" src="invalid.png"></img> 
   </div>
 `
})

Vue.component('kf-combo', {
  mixins: [compBase],
  props: {
    'value': { type: [String, Number] },
    "items": Array
  },
  methods: {
  },

  template: `
   <div class="flex-row" >
   <h3 class="title-col" >{{ text }}:</h3>

   <select 
    class="value-col inp " :class="{ invalid: !valid }" :title="notValidText" 
    v-model="valueInt"
    v-on:input="valChanged($event)"
  >
  <option v-for="item in items" :key="item.id"   v-bind:value="item.id" >{{item.name}}  </option>
  </select>
   <img v-if="!valid" src="invalid.png"></img> 
   </div>
 `
})

Vue.component('kf-num', {
  mixins: [compBase],
  props: {
    'value': { type: [Number,String] },
  },
  methods: {
    virtChange(val){
      val = reFormatNum(val)
      this.$emit('input', val)//event to parent
    }
  },
  mounted: function () {
    this.valueInt = formatNum(this.value)
  },
  template: `
   <div class="flex-row" >
   <h3 class="title-col" >{{ text }}:</h3>
   
   <input 
    class="value-col inp " :class="{ invalid: !valid }" 
    :title="notValidText" 
     v-bind:value="valueInt"
     v-on:input="valChanged($event)"
     onblur="onBlur(this)"
     onfocus="onFocus(this)"     
   />

   <img v-if="!valid" src="invalid.png"></img> 

   </div>
 `
})


// import { w2field, query } from
// General

//new w2field('float', { el: query('#eu-float')[0], groupSymbol: ' ', precision: 3 })

function onBlur(control) {
  control.type = "text";
  control.value = formatNum(control.value)

}

function onFocus(control) {
  control.value = reFormatNum(control.value)
  control.type = "number";

}

function formatNum(val) {
  return numeral(val).format('0,0.00').replaceAll(',', ' ');
}

function reFormatNum(val) {
  return val.replaceAll(' ', '');
}