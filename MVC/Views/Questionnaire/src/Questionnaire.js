import colors from "vuetify/lib/util/colors";

export class QuestionnarieBo  {
	constructor(QuestionnaireDto) {
        this.Id=QuestionnaireDto.Id
        this.Name=QuestionnaireDto.Name

        this.Questions=[]

        console.log(QuestionnaireDto);
		for (var i in QuestionnaireDto.Questions) {
			const item = QuestionnaireDto.Questions[i]
			this.Questions.push(new Question(item));
		}
	}
	// GetChanges() {

	// 	let changes = [];
	// 	for (var i in this) {
	// 		const item = QuestionnaireDto[i]
	// 		if (!/^\d+$/.test(item.Id)) {
	// 			changes.push(item);
	// 		} else {
	// 			if (item.IsChanged()) {
	// 				changes.push(item);
	// 			}
	// 		}
	// 	}
	// 	return changes;
	// }

}
export  class Question {

	constructor(propDto) {
       console.log(propDto)
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
