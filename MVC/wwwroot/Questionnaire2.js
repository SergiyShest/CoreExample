let vue_ = new Vue({
    el: "#app",
    data: {
        yes: null,
        no: null,
        Item: {

            Name: null,
            Phone: null,
            Email: null,
            Check: null
        }
        , rules: {
            required: (value) => !!value || "Required.",
            counter: (value) => (value && value.length <= 30) || "Max 30 characters",
            fieldsNumb: (value) => (!value || value && value <= 5) || "Max 5 ",
            phone: (value) => {
                if (value) {
                    value = value.replaceAll(' ','').replaceAll('-','')
                    const res = /^\d{8,10}$/.test(value)
                    if (!res) return "Telephone number mast conatins 8-10 numbers, possilble white space or hyphens";
                }
                return true;
            },
            email: (value) => {
                if (value) {
                    value = value.replaceAll(' ','')
                    const res = /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(value)
                    if (!res) return "E-mail must be valid";
                }
                return true;
            }
        },

    },
    watch: {
        yes(y) {
            if (y) {
                this.no = false
                this.Item.Check = false
            }
        },
        no(n) {
            if (n) {
                this.yes = false
                this.Item.Check = true
            }
        },
    },
    methods: {

        validation() { 
          var valid = true;
            for (var i = 0; i < 3; i++){
                const comp = this.$children[i];
                comp.Validate();
                if (!comp.valid) {
                    valid = false;
                }
            }



            return valid
        }
        ,
        send() {
           
            if (!this.validation())
            {
                alert("Please fill in the required fields");
            }
            else {
                var path = "Questionnaire2/SaveAnswer?sessionId=" + SessionId
                this.fetch(this.ok, path, this.Item)
            }
        },
        ok() {
            console.log('ok')
            document.location = document.location.origin + '/Questionnaire2/End'
        },

        //������������� ������� ���������/�������� ������
        fetch: function (execFunction, pathEnd, data = null) {
            this.loadingData = true;
            const fetchRef = execFunction;
            const path = document.location.origin + '/' + pathEnd
            const json = JSON.stringify(data);
            fetch(path,
                {
                    method: 'POST', // *GET, POST, PUT, DELETE, etc.
                    mode: 'no-cors', // no-cors, *cors, same-origin
                    body: json, //sending data
                    referrer: 'unsafe-url',
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                .then((response) => {
                    this.loadingData = false;
                    if (response) {
                        return response.json();
                    }
                })
                .then((retData) => {
                    if (retData) {
                        {
                            console.log(retData)
                            fetchRef(retData)
                        }
                    }
                    this.loadingData = false;

                }).catch(error => {
                    this.loadingData = false;
                    console.error("Error while getting server data: " + path + ' :' + error)
                });

        }
        ,
    },
    mounted: function () {
    }
});//.$mount('#app')

function submitform() {
    vue_.send()
}