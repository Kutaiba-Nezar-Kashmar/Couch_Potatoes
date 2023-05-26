import Keyword from "./keywords";
import Language from "./language";
import Genre from "./genre";
import Video from "./Video";
import Poster from "./Poster";
import Backdrop from "./Backdrop";

export default interface Movie {
    id: number;
    title: string;
    imageUri: string;
    summary: string;
    backdropUri: string;
    tmdbScore: number;
    tmdbVoteCount: number;
    releaseDate: string;
    runTime: number;
    tagLine: string;
    isForKids: boolean;
    status: string;
    homepage: string;
    trailerUri: string;
    budget: number;
    revenue: number;
    keywords: Keyword[];
    languages: Language[];
    genres: Genre[];
    videos: Video[];
    posters: Poster[];
    backdrops: Backdrop[];
}

