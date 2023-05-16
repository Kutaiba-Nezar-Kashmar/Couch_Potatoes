export enum Gender {
    MALE = "Male",
    Female = "Female",
    OTHER = "Other",
}

export interface User {
    id: string;
    username: string;
    email: string;
    gender: Gender;
}
