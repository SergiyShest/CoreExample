export class Models extends Array {
	constructor() {
		super();
        console.log('ok');
	}
	addModel(model)
	{
		console.log(model)
	}

}
export  class Model {

	constructor(propDto) {

		this.Id = propDto.Id;
		this.ParentId = propDto.ParentId;

        this.Name = propDto.Name;
        this.BdName= propDto.Name;
        
        this.Shema=propDto.Shema;
        this.BdShemaJson= JSON.stringify(propDto.Shema)

        this.Options=propDto.Options
        this.BdOptionsJson= JSON.stringify(propDto.Options)
	}

	IsChanged() {

		return (

			this.Name != this.Name ||
			this.BdShemaJson !=  JSON.stringify(this.Shema)||
			this.BdOptionsJson != JSON.stringify(this.Options)
		);

	}


}
