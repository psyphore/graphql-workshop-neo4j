// CREATE DATABASE CyberpunkGenre
// USE EpicGenre

CREATE (Ghost:Movie {title:'Ghost in the Shell: SAC_2045', released:2020, tagline:''})
CREATE (Mary:Person {name:'Mary Elizabeth McGlynn', born:0})
CREATE (William:Person {name:'William Knight', born:0})
CREATE (Richard:Person {name:'Richard Epcar', born:0})
CREATE (Crispin:Person {name:'Crispin Freeman', born:0})
CREATE (Michael:Person {name:'Michael McCarty', born:0})
CREATE (Dave:Person {name:'Dave Wittenberg', born:0})
CREATE (Bob:Person {name:'Bob Buchholz', born:0})
CREATE (Dean:Person {name:'Dean Wein', born:0})
CREATE (Melissa:Person {name:'Melissa Fahn', born:0})
CREATE (Cherami:Person {name:'Cherami Leigh', born:0})
CREATE (Keight:Person {name:'Keight Silverstein', born:0})
CREATE (Roger:Person {name:'Roger Craig Smith', born:0})
CREATE (Armen:Person {name:'Armen Taylor', born:0})
CREATE (Max:Person {name:'Max Mittelman', born:0})
CREATE (Kenji:Person {name:'Kenji Kamiyama', born:0})
CREATE
(Mary)-[:ACTED_IN {roles:['Motoko Kusanagi']}]->(Ghost),
(William)-[:ACTED_IN {roles:['Daisuke Aramaki']}]->(Ghost),
(Richard)-[:ACTED_IN {roles:['Batou']}]->(Ghost),
(Crispin)-[:ACTED_IN {roles:['Togusa']}]->(Ghost),
(Michael)-[:ACTED_IN {roles:['Ishikawa']}]->(Ghost),
(Dave)-[:ACTED_IN {roles:['Saito']}]->(Ghost),
(Bob)-[:ACTED_IN {roles:['Paz']}]->(Ghost),
(Dean)-[:ACTED_IN {roles:['Borma']}]->(Ghost),
(Melissa)-[:ACTED_IN {roles:['Tachikoma']}]->(Ghost),
(Cherami)-[:ACTED_IN {roles:['Purin Esaki']}]->(Ghost),
(Keight)-[:ACTED_IN {roles:['Standard']}]->(Ghost),
(Roger)-[:ACTED_IN {roles:['John Smith']}]->(Ghost),
(Armen)-[:ACTED_IN {roles:['Chris Otomo Tate']}]->(Ghost),
(Max)-[:ACTED_IN {roles:['Takashi Shimamura']}]->(Ghost),
(Kenji)-[:DIRECTED]->(Ghost);