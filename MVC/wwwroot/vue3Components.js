function ConvertDate(value){
  return value
}
function numOnBlur(control) {
  control.type = "text";
  control.value = formatNum(control.value)

}

function numOnFocus(control) {
  control.value = reFormatNum(control.value)
  control.type = "number";

}

function formatNum(val) {
  return numeral(val).format('0,0.00').replaceAll(',', ' ');
}

function reFormatNum(val) {
  return val.replaceAll(' ', '');
}

// Свойства (Props)
// text - тип String, текст для отображения.
// req - тип Boolean, указывает, требуется ли заполнение поля.
// valudateonload - тип Boolean или String (возможные значения: true, false), указывает, должна ли производиться валидация при загрузке.
// requretext - тип String, текст сообщения об ошибке при отсутствии значения в обязательном поле.
// rules - список правил валидации.
// inputStyle - тип Object или String, стили для поля ввода.
// inputClass - тип String, класс для поля ввода по умолчанию.



const componentBase = {
    props: {
        'text': String,
        'requre': Boolean,
        'valudateonload': { type: [Boolean, String], default: false },
        'readonly': Boolean,
        'requretext': { type: String, default: 'Значение должно быть заполнено!!!' },
        'rules': { type: Array, default: () => [] },
        'inputStyle': { type: [Object, String] },
        'inputClass': { type: String, default: 'def-inp' },
    },
    setup(props) {
        const valueInt = ref(null);
        const valid = ref(true);
        const externalReadonly = ref(false);
        const notValidText = ref(null);

        const inputClasses = computed(() => ({
            'invalid': !valid.value,
            [props.inputClass]: props.inputClass
        }));

        const valChanged = (event) => {
            virtChange(event.target.value);
        };

        const virtChange = (val) => {
            valueInt.value = val;
            emit('input', val);
        };

        const SetReadonly = (val) => {
            externalReadonly.value = val;
        };

        const Validate = (val) => {
            if (typeof val === "undefined") {
                val = value.value;
            }
            valid.value = true;
            const errList = [];
            notValidText.value = '';

            if (props.requre && !val) {
                valid.value = false;
                notValidText.value = props.requretext;
            } else {
                try {
                    if (props.rules) {
                        props.rules.forEach(element => {
                            const valResult = element(val);
                            if (valResult !== true) {
                                errList.push(valResult);
                                valid.value = false;
                            }
                        });
                        if (!valid.value) {
                            notValidText.value = errList.join('\n');
                        }
                    }
                } catch (error) {
                    console.error("Error while validation rules executing: " + error);
                }
            }

            console.log(props.text + ' valid = ' + valid.value);
        };

        watch(() => props.value, (newValue) => {
            valueInt.value = newValue;
            Validate(newValue);
        }, { immediate: true });

        const mounted = () => {
            valueInt.value = props.value;
            if (props.valudateonload == true) {
                Validate(props.value);
            }
        };

        return {
            valueInt,
            valid,
            externalReadonly,
            notValidText,
            inputClasses,
            valChanged,
            virtChange,
            SetReadonly,
            Validate,
            mounted
        };
    }
};
export const KfField ={
    mixins: [componentBase],
    props: {
        'value': { type: String },

    },
    methods: {
        valChanged(event) {
            this.$emit('input', event.target.value);
        }
    },

    template: `
      <input 
          :class="inputClasses" 
          :style="inputStyle"
          :title="notValidText" 
          v-bind:value="value"
          v-on:input="valChanged($event)"
      />
  `
}






export const KfButton = {
  props: {
      'text': null,
      'image':null
  },
  computed: {
      imgSrc() {
          if(this.image) return /images/ + this.image+".png";
    }
  },
  template: `<button
class="kf-button"

>
<img v-if="image" :src="imgSrc" class="kf-button-image" />{{text}}<slot></slot>
</button>`
}//kf-button  

