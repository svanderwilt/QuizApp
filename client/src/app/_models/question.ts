export interface Question {
  id: number;
  questionText: string;
  choices: string[];
  answer: string;
  hint: string;
  photoUrl: string;
}
