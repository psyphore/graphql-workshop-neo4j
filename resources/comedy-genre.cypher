// CREATE DATABASE ComedyGenre
// USE EpicGenre

CREATE (Transformers:Movie {title:'Transformers: Combiner Wars', released:2016, tagline:''})
CREATE (Anna:Person {name:'Anna Akana', born:0})
CREATE (Jon:Person {name:'Jon Bailey', born:0})
CREATE (Michael:Person {name:'Michael Green', born:0})
CREATE (Charlie:Person {name:'Charlie Guzman', born:0})
CREATE (Ricky:Person {name:'Ricky Hayberg', born:0})
CREATE (Amy:Person {name:'Amy Johnston', born:0})
CREATE (Jason:Person {name:'Jason Marnocha', born:0})
CREATE (Lana:Person {name:'Lana McKissak', born:0})
CREATE (Ben:Person {name:'Ben Pronsky', born:0})
CREATE (Patrick:Person {name:'Patrick Seitz', born:0})
CREATE (Frank:Person {name:'Frank Todaro', born:0})
CREATE (Abby:Person {name:'Abby Trott', born:0})
CREATE (Kenji:Person {name:'Kenji Nakamura', born:0})
CREATE
(Anna)-[:ACTED_IN {roles:['Victorion']}]->(Transformers),
(Jon)-[:ACTED_IN {roles:['Optimus Prime']}]->(Transformers),
(Michael)-[:ACTED_IN {roles:['Metroplex']}]->(Transformers),
(Charlie)-[:ACTED_IN {roles:['Menasor']}]->(Transformers),
(Ricky)-[:ACTED_IN {roles:['Comutron']}]->(Transformers),
(Amy)-[:ACTED_IN {roles:['Maxima']}]->(Transformers),
(Jason)-[:ACTED_IN {roles:['Megatron']}]->(Transformers),
(Lana)-[:ACTED_IN {roles:['the Mistress of Flame']}]->(Transformers),
(Ben)-[:ACTED_IN {roles:['Rodimus Prime']}]->(Transformers),
(Patrick)-[:ACTED_IN {roles:['Devastator']}]->(Transformers),
(Frank)-[:ACTED_IN {roles:['Starscream']}]->(Transformers),
(Abby)-[:ACTED_IN {roles:['Windblade']}]->(Transformers),
(Kenji)-[:DIRECTED]->(Transformers);