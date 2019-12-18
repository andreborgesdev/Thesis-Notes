# Docker

## Containers

### Bad old days

Applications run businesses

Well, applications run, for the most part, on servers. And back in the day, I'd say definitely early to mid 2000s, most of the time we were doing one app per server. And by servers, I mean big, expensive.

Hey, the business needs a new application for whatever reason, maybe a new product launch or something. Whatever though, the business needs a new app. Well, that means IT needs to go out and buy a new server. That comes with an upfront CapEx cost, but don't forget, it's also got a bunch of OpEx costs. I mean, power and cooling isn't free, and neither is hiring people to build and administer stuff. Okay, but you know what? Want kind of server does this new application require? How big does it have to be and how fast? And I can tell you from sorry experience, the answers to questions like these were almost always nobody knows. Seriously! Nobody ever knew how big or fast a server had to be. So, IT did the only reasonably thing. They erred on the side of caution, and they went big and fast. And rightly so, right? I mean, the last thing anyone wanted, including the business, was poor performance. I mean, imagine it, unable to carry out business and potentially losing customers and revenue all because IT cheeked out on a server that wasn't fast enough. No, not happening on my shift. So IT bought big and fast, and yeah, you probably know, 99 times out of 100 we ended up with a bunch of massively overpowered servers running at like 5% or 10% of what they were actually capable of. A proper shameful waste of company capital and resources.

### VMWare

Then along came VMware, and oh my goodness did the world change for the better! Almost overnight we had this technology that let us take those same over-specced physical servers and squeeze so much more out of them. Literally, a load more bang for the company's buck. So, I guess, well done IT operation's guys. I never doubted for one second that you always knew that those overpowered servers would one day become useful.

Instead of dedicating one physical server to one lonely app, suddenly we could safely and securely run tons of apps on a single physical server.  That scenario of the business coming and saying, hey, we're growing, expanding, diversifying, whatever, and we need a new application, well it's no longer an automatic purchase of an expensive new server. We've already got these servers over here that are barely doing anything. We'll just put the app on one of them. And like I say, almost overnight, though let's not forget, I mean, VMware is a company, and hypervisor technology in general is way more than a decade old now. So, it's not really overnight. It did take time. But here we are in a day and age where 999 times out of 1, 000 we only buy a new server when we genuinely need one. We are properly squeezing stuff onto our servers and sweating those company assets.

___Marts___

So, as good as the VMware and the hypervisor model is, it's got a few shortcomings.

We take a single physical server, and I'm going with a slightly more detailed diagram this time, but we're still high level. So, this is our server. It's got processes, memory, and disk space, and we know we can run a bunch of apps on it. Now, I'm only showing four here to keep the diagram simple. Anyway, to run these four apps, we create four virtual machines, and each one of these is essentially a slice of the physical server's hardware. So let's call this here virtual server 1, and we might've allocated it, I don't know, 25% of the underlying server's processing power. Remember, we're just big picture here. So maybe 25% of CPU, 25% of memory, and 25% of the physical server's disk space. And then, you know what, let's just say we did the same for the rest. Well these are all slices of the real resources in the physical server below. Then, each one of these virtual machines needs its very own dedicated operating system. So, that's four installations of usually Windows or Linux, each of which steals a fair chunk of those resources, CPU, memory, and disk, and it steals them just in order to run. We've not got any applications running yet. This is just the operating system stealing those resources. But that's not all. You may even need four operating system licenses. So right there we've got potential cost already in resources and budget that, I don't know, it just feels like is a waste. I mean, look, as cool as operating systems are, they're a necessary evil. If we could safely and securely run our apps directly on the server hardware without needing an operating system, I tell you what, we definitely would. But back on track. It's not just any potential cost of licensing the operating systems. Each and every one needs feeding and caring for, so admin stuff like security patching, updating, maybe anti-virus management. There's like this whole realm of operational baggage that comes with each one. And VMware and other hypervisors, as great as they absolutely are, they don't do much to help us with this. So, yeah, VMware and the hypervisor model, it changed the world into a much better place, but there's still issues, and there's still gains to be made, which leads us nicely on to containers.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/VMWare_Weaknesses.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/VMWare_Weaknesses_2.png?raw=true)

### Containers

So, you know how a virtual machine manager, or a hypervisor, how it grabs physical resources like CPU, RAM, storage, networks, then it slices them into virtual versions, so virtual CPU, virtual RAM, virtual NICs, all that goodness, and then it builds virtual machines out of them, virtual machines that look, smell, and feel like normal, physical servers? Well, not so with containers. Now, keeping this somewhat high level here, right, instead of slicing and dicing physical server resources, Docker and other container engines slice and dice operating system resources. So they slice up the process namespace, the network stack, the storage stack, or the file system hierarchy actually. In effect, right, every container gets its own PID 1, process ID 1. Every container gets its own root file system. That's obviously slash on Linux and C on Windows. So, hypervisor virtualization virtualizes physical server resources and builds virtual machines. Container engines like Docker, they're more like operating system virtualization. They create virtual operating systems, assign one to each container, inside of which we run applications, and they're way more lightweight than VMs.

Instead of installing a hypervisor down here and then four virtual machines and operating systems on top, each with its own baggage and overhead remember, well instead of all that, we install one operating system, just one. Then, on top of that, we create four containers. Now we'll come to it in a minute, but each of these containers is a slice of the operating system. Well, it's inside these containers that we run our apps, one-to-one again, one app per container.

I am purposefully drawing the containers smaller than I drew the virtual machines because they actually are smaller, and they're more efficient. Though, aside from that, the model kind of looks similar, right? In fact, let's see a side-by-side comparison. See how on the left here on top of the hypervisor we create a virtual machine? Well all that is is a software construct dressed up to look and feel exactly like a physical server. So like we said before, each one's got its own virtual CPUs, virtual RAM, virtual disks, virtual network cards, the whole shebang. Then on top of that, we said we'd install an operating system, and to each one of those operating systems, the virtual machine below it looks exactly like a physical server. It doesn't know the difference. Anyway, look, we already said that these operating systems have CapEx and OpEx costs. I mean, there's patching, upgrading, driver support, all that stuff. But look here. Each operating system also consumes resources from the physical server, effectively stealing resources. So each and every operating system steals CPU. It steals memory, and it steals disk space.

We could call this model the hungry operating system model. Each and every one is eating into everything, admin time, system resources, budgets, you name it. Oh, and you know what? Gets worse. Each one is a potential attack vector. So seriously, somebody remind me why we have them. Anyway, look, back to the container model here. It's only got one operating system. So, take a physical server, install an operating system, and then we essentially carve, or slice, that operating system into secure containers. Then, inside the containers we run an app. Net result: We get rid of pretty much all this fat here. It's just gone, meaning we've got all of this free space over here to spin up more containers and more apps for the business. 

Oh, and you know what? These apps in the containers here, they start like, I don't know, just so fast. It's ideal for situations where you're spinning things up and tearing things down on demand because there's no virtual machine and no extra operating system to boot before your app can start. No, in the container model, the OS is down here, and it's already running. So all of these apps up here in the container model are securely sharing a single operating system down here. Net net, most containerized apps spin up in probably less than a second, and you've only got one operating system that's stealing resources and demanding admin time.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Containers.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Containers.png?raw=true)

Now, it doesn't matter where or what that machine is. So it could be a virtual machine in the cloud or a bare-metal server in your datacenter or even your laptop running Docker Desktop. It really doesn't matter. Docker is Docker. It runs on VMs, bare metal, your laptop, whatever. In fact, do you know what? Let's drop our picture in up here. So I'm logged on to the host here, and it's got Docker installed. Now Docker does Linux and Windows, and generally speaking, at the kind of high level we're at at least, Docker on Linux will only run Linux apps, and Docker on Windows only runs Windows apps. Now, look, there are ways to get Linux apps running on Docker on Windows, fair enough, but for us right now, at the kind of level we're at, it's really best to think of Linux apps running on Docker on Linux and Windows apps run on Docker on Windows.

you can think of an image as a prepacked application, or if you're a techie guy, maybe think of it as like a VM template. Basically, it's got everything wrapped up into a single bundle that you need to run an application. This one happens to contain a web server that runs some static content. So, to fire up a container from this image, we'll use this long command, which in case you're interested says run me a new container, base it off of the image that I just downloaded, call it this name, and then expose it on this network port. Yeah, there's other options in there, but for us right now, this is all we need to know.

This number is the unique ID of the container, and it tells us that it's already up and running. So, I don't know how fast that was, but less than a second probably

Now, there's commands and the likes to get details of running containers, obviously, but all that we need to know is the IP address of our server here, that's this number up here, and that we exposed it on port 8080. So, if we switch to a new browser tab here and put in that IP and port, boom, there is our web server.

An embedded command that would automatically start that web server when we spin the image up as a container. Well, once that image was downloaded, and you download these from container registries like Docker Hub, which for want of a better analogy is a bit like the App Store, but just for containerized apps.

Anyway, once we've got the image, we told Docker to fire it up as a container. We gave it a name, and we exposed it on a network port. And you know what? Docker just made it happen, and fast. Then, obviously, we verified it with a browser. Good stuff. But you know what? Because containers are a lot like virtual machines, just faster and more lightweight, well, we can stop them like this, and if we go back to the web page and hit refresh, we see, as expected, it's not running anymore. But back here to start it again, and refresh the browser again, and we're back in business.

### Microservices

Legacy apps, or monolithic apps as we sometimes call them, these are those monstrous apps where everything that the app does is pretty much baked into a single binary, which is just a fancy name for a computer program. So everything lumped into a single program. Maybe your app has a web front end, a shopping cart, inventory manager, search, authentication, I don't know, a checkout service, you name it. In the monolithic design, all of that functionality gets baked into a single program, and without getting into detail, it's just a nightmare from a developer perspective. If you want to update or fix, let's say, just the search part of the app, it is a whole big exercise on the entire code base. So, you're hacking the entire app, and you're testing, and you're recompiling the whole thing. Not a lot of fun, and you know what, more than a bit risky. And on the operations front, if you've got an issue, let's just say with the same search functionality again, the only way to roll out a fix, because everything's lumped into a single program remember, so the only way to roll out a fix is to take the entire app down. Good luck getting the business to agree to that.

Fortunately, cloud native and microservices on the other hand, these break out all of those different components and make each one its own little mini app or mini service. I mean, they all still talk to each other to make the full app experience, but updating that search feature, all of the sudden, that just became way easier for the developer and the operator. So now the developer only needs to touch the search code when it updates the search feature, and ops, they only need to roll out the new version of the search service. No more taking the entire beast down just to update one part. And you know what? That's the essence of microservices and cloud native, build, deploy, and manage apps in a way that lends itself to modern business requirements, or cloud computing requirements as we often call them. So, no, it isn't really anything to do with deploying on the cloud. I mean, you can absolutely run a cloud-native app in your on-prem datacenter. You see, cloud native is all about how the app's built and managed, so we can do things like scale the front end independent of the back end. And like we said, you can iterate on each feature independently.

In a way, containers are virtualization 2.0. They improve on nearly everything offered by hypervisors, and they pave the way for more modern cloud native and microservices applications. Though, do you know what? Don't expect them to replace VMs, I mean, not entirely because, well, I mean, in a lot of cases, they'll live side by side. I mean, sure, plenty of people are container only, especially startups and those people that are 100% in the public cloud, but in most enterprises, and a lot of other places, we'll be seeing containers and VMs sitting side by side, and you know what, even the occasional mainframe lurking around in the background.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Containers_Summary.png?raw=true)

## Docker

Docker is both the name of the company and the technolohy

### Docker Inc

It's a technology startup from San Francisco, and it's the main sponsor behind the open-source container technology with the same name.

Docker the company didn't actually start out life as Docker, nor was it really anything to do with changing the way that we build, ship, and run our applications. Originally, it was a company called dotCloud that provided a developer platform on top of Amazon Web Services. So, you know, like taking AWS and then layering this kind of uniform developer experience on top. Only that wasn't working so much as a business, and at around about 2013 they really needed something different. And it just so happens in one of those twists of fate they'd been using containers to build their platform on top of AWS, and, and this is the important bit, they had this home-grown tech that they built as an internal tool to help them spin up and manage their containers. And cutting a long story short, and I wasn't there myself, but this is the gist, they needed something new. They looked at this in-house tech for building containers and thought what if we give this to the world and build a business around it? Well obviously, that in-house tech was Docker, and here we are today where Docker has literally changed the technology world in a similar way to VMware, though arguably, Docker's changed things in a deeper and a more fundamental way.

The name Docker actually comes from a British colloquialism that's a conjunction of dock and worker. So somebody who works at a dock, or a shipping port, you put the two together, get rid of the work, and you get Docker. And I really like it. It's short and catchy. Anyway, like we said, around 2013 the company called dotCloud made a humongous pivot, and it changed its business from being this company that provided a developer platform on top of AWS to a company that changed the way we build, ship, and run software.

They gave the world the gift of Docker and easy-to-use containers, and these days they're in the business of orchestrating and supporting containerized apps at scale with a focus on enterprises.

### Technology

Containers are like fast lightweight virtual machines, and Docker makes running our apps inside of containers really easy.

The Docker application, if you will, is open source, and like more open-source software these days, it lives on GitHub.

We call apps running in containers containerized apps.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Technology.png?raw=true)

Each services gets coded separately, and each one lives in its own container.

We'll take the code and build it into a Docker image. Now, an image is like a stopped container or maybe a template for how to build a container. Anyway, we'll build an image. Then we'll push that to a registry. After that, we'll start a container from it.

I'm just going to push the image to Docker Hub, but you can have your own on-prem or private registries.

And you use that image to spin up your app as containers.

But, as simple as it is, it is absolutely key to moving to a modern cloud-native microservices design, which I know is a bunch of buzzwords, but it's all vital if you want your business and your applications to be able to roll with the demands of the modern world. 

Okay, well, it's all well and good running a single container on your laptop like we've just shown you. It's a whole different world though doing it at scale. And you know what? Scale is where the real world is.

So, to help us with this, there's two things I'll mention here. I mean, there's other options as well, but for us in this course, there's Docker Swarm and there's Kubernetes. Now, Swarm's great, and we cover it in our Getting Started with Docker and Docker Deep Dive courses, but for us now on this course, we're going to focus on Kubernetes because to be honest, it's where most of the action is.

## Preparing for the thrive in a container world

### Individual Preparation

The two things you need to survive and thrive are knowledge and experience.

The point is there are no excuses for not getting your hands filthy with dirt.

Online - play with Docker and Play with kubernetes

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Individual_Preparation.png?raw=true)

### Organizational Preparation

This one is a bit trickier, but it's still very doable. Well, first and foremost is acceptance. Your teams and organizations have to accept that containers are coming.

After that, start thinking and talking about good areas to start using them. Now, generally speaking, developers are going to love them, and a great place for developers to start is continuous integration and continuous delivery. But, keep a tag on things because the chances are they'll like them so much they'll start using them anywhere they can, which is good in the long run, it just needs to be done right.

What a lot of companies do, especially the bigger ones, is they set up some kind of SWAT team, and they give them a new project or some area of the business that's a good fit for a testing ground. So like you section off this area of the business or whatever for a specialized team, and you have them get into the whole thing, Docker, Kubernetes, microservices, you name it, the whole shebang, and you get them to learn it, and you get them to deploy it. And once they've done that maybe once or twice, then you get them to become ambassadors or whatever for the wider company. So like a seeding team. Pull it off in a new project or two, then deliver it to the wider organization.

developers are great, but it's important not to ignore infrastructure and ops, especially I think with Kubernetes because that's arguably got more on the ops front, like deploying and then also managing your apps. And guess what? For this to work in your production environments, the same old production rules apply. I'm thinking things like you're going to want resilient infrastructure to run these new apps on, you're going to want monitoring, you're going to want logging, you're going to need orchestration, and as always, do not leave it until the last minute.

The golden rule here really is just to talk. Get dev and ops talking, get management talking, and then get doing. And like we said, start small, but dream big.

So recapping, a small specialist team, have them work on something small, but take the holistic view, and then when they've done it successfully, seed it throughout the rest of the business. It's a tried and tested approach.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Organizational_Preparation.png?raw=true)

## Suitable Workloads

### Stateless and Stateful

A question I still get asked about containers is whether or not they can be used for stateful apps, so apps that persist data, or if they're just good for stateless.

Since at least 2018, both Docker and Kubernetes have gotten really good at doing stateful.

a stateful app, or a stateful service, is one that absolutely has to remember stuff. Like if a stateful app stops or crashes or the node it's running on dies, well it abso-freaking-lutely has to come back up without forgetting anything, and a database is the usual example. So when you first fire up a database, it probably looks something like this. So the database app is running in a container here on node 2, and it's using a volume to actually store the data. That's our state. And when it's very first created, it's empty, but as things crack on, it starts storing data. Then, if things go pop, for whatever reason, it doesn't matter, but what does matter is that restarting the service may be over here, it absolutely has to come back up with all the data that was previously stored. If you started here fresh again with no data, well what's the point? So for us, that's stateful. It has to remember stuff. 

Stateless, on the other hand, that's easy. It doesn't remember stuff. So, whatever you started with on day one, maybe a web server with some static content, if it runs for two weeks, at the end of those two weeks, it looks exactly the same as it did on day one, like nothing new has been updated or stored. So if that goes bang, we just bring it back up exactly how it was on the first day two weeks ago.

The general story these days is that Docker and Kubernetes are actually pretty darn good at both. I mean, they're the absolute business when it comes to stateless, but without any trace of stretching the truth, they are really good at stateful as well.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Stateless_Stateful.png?raw=true)

### Low-hanging fruit

Now, I don't think there's any doubt that there's a huge push towards modern cloud native and microservice designs and architectures. The premise is modern businesses need to be agile and a whole bunch of other buzzwords, but buzzwords aside, these are facts. Modern businesses needs to be more reactive and more adaptive than ever, and modern businesses are for the most part the sum of their applications, at least to the extent that crappy old sluggish apps equals crappy old sluggish businesses for the most part.

We need scalable adaptable businesses. The market's demanding it. Well, clouds are providing the infrastructure, and Docker and Kubernetes are providing the tools for building the apps.

Now, you might remember from the module on containers we said that VMware and hypervisors revolutionized IT. The emphasis on IT there maybe rather than apps and businesses. Anyway, they dragged IT from the dark ages of wasted server resources, whereas now we're in the modern world where we're pushing resource utilization like we never pushed it before. Fabulous! Only, the VM thing is a bit of a two-edged sword. On the good side, it let us lift our existing applications from the physical world and drop them straight into the virtualized world, but on the bad side, it let us lift our existing applications from the physical world and drop them straight into the virtualized world.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Workloads.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Workloads_2.png?raw=true)

When we come to containerize our apps, we really should be rethinking and refactoring them. Because like we said, business requirements have changed, and with clouds and containers, we've got everything we need now to build much better apps. And really, I am not talking about just better for IT or for me as a techie. I'm genuinely talking about better for the business and better for the customer, noticeably better.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Workloads_3.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Workloads_4.png?raw=true)

The point is yes, it's the way forward, and yes, we absolutely want to do business on those kinds of terms, but yeah, it takes pain and effort to get there.

### State and Legacy Apps

Like we've said, Docker and Kubernetes are absolutely magic when it comes to stateless workloads, but when we say that, it could be misconstrued to imply, and it often has been, that they are not good for stateful or traditional apps, which, let's be fair, is still the staple of most enterprises. Well, the good news is it is not true that containers can't do stateful or even traditional heritage apps. You know what? It was just that these kinds of workloads are harder, and I think this is the case with just about anything that's new. The easy stuff gets done first. But guess what? Containers are not new anymore. This stuff is growing up fast, and as Docker and Kubernetes have matured, they've added the stuff that's needed for stateful and traditional apps. I mean, on the Docker technology front, volumes and persistent storage, that's come on leaps and bounds. And the same for Kubernetes. It's got a pretty comprehensive persistent storage subsystem. Now, while we're on with Kubernetes, it's also got a ton of other features and objects for stateful services. So, things like the Kubernetes Deployment object, that's great for stateless work, and don't stress if some of the terminology is new here. The point is Kubernetes has, and probably always has had, the stuff for stateless workloads. But on the stateful front, well, as well as things like persistent storage, there's stateful sets and other stuff, all of which are core to Kubernetes. 

The primitives, and the objects, and everything else necessary, integrations into external storage systems, you name it, it's all there so you can do stateful work.

So, Docker and Kubernetes definitely does stateful. Now, on the legacy, or heritage application front, if you're not ready to refactor your apps, but maybe you do want to move to a container platform, well, at least one example, of which there are others, but Docker, Inc. for an age now has had its modernizing traditional apps program where they make it super simple just to lift and shift some of your legacy apps into containers. Now, it's not an end goal in and of itself, but it is a step one in getting onto a modern container platform.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Stateful.png?raw=true)

At the end of the day, technology is always about either the business or the project, and modern businesses and projects need to morph and grow and deal easily with change. So it stands to reason that our technologies need to do the same. Like, if our tech can't adapt and grow, then our businesses have got no chance. No sweat though. We're living in a golden age of technology where we've got all the tools we need. I mean, cloud platforms are providing us with things like infrastructure on demand, while Docker and Kubernetes give us the tools to build agile scalable apps. So, Dynamic infrastructure and tools to build dynamic apps, and I mean entire apps.

They're great for new modern apps, the stateless and stateful bits, and they're also an option for some of your older heritage apps, and they can totally sit alongside VMs and functions within the same app.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Workloads_5.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Workloads_6.png?raw=true)

## Enterprise and Production Readiness

### Docker

So, at its core, Community Edition and Enterprise Edition are the same. I'm talking about the code that starts and stops containers, that's the same, but bolted on around all of that, Enterprise Edition gets a ton more.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Editions.png?raw=true)

### Kubernetes

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Kubernetes_Production.png?raw=true)

The cloud provider hosts and manages the hard Kubernetes stuff, and we just deploy our apps to it.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Kubernetes_Production_2.png?raw=true)

Kubernetes is a gigantic project, way bigger than Docker. I mean, the scope and breadth of what Kubernetes can do is awesome. Look, and I'm a Brit. I don't use that word very often. But honestly, the scope of what Kubernetes can do truly is awesome, and it's growing all the time, which, okay, is great. But no surprises, it's got its challenges, one of which is just keeping track of features. I mean, some features, they've been around for ages and they're rock solid, whereas others, you probably shouldn't even touch them if your life depends on it. Well, fortunately, to help us keep track of this, every Kubernetes feature goes through a set of well-defined stages.

GA is the gold standard, or the stamp of approval. Anything in GA is here for the long game, and it should be stable.

Alpha, that's scary; beta, that's for the brave and the early adopters; and GA, that's for the rest of us.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Kubernetes_Production_3.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Kubernetes_Production_4.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Kubernetes_Production_5.png?raw=true)

### Container Ecosystem

Okay, a quick word on the container ecosystem because Docker and Kubernetes, they are by no means the entire picture. If you go to any of the major events like DockerCon or KubeCon, you will see a ton of companies building up around them and filling in the gaps. You know, things like monitoring, and security, and machine learning. Tons of it. There's companies springing up offering just about everything you'd need to augment and enhance your Docker and Kubernetes environments. Now, I'm going to name any specific companies because, well, I guess it wouldn't be fair, but also, some of them just won't last, and that's an important point to consider. I mean, sure Docker and Kubernetes' technologies are going to be here for the long term, but some of the companies in the ecosystem certainly won't, and you're going to want to consider that when you're choosing who to use. But, that said, some of them are solid companies with great products, and you could do worse than checking them out and seeing where they can help.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Container_Ecosystem.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Container_Ecosystem_2.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Production.png?raw=true)

## Orchestration

On their own individually, they're not really that special. It's when they come together as a team that the magic happens. But in order to work as a team, they need organizing, dare I say orchestrating?

the team is made up of individuals, and each one's got their own job. Some guys block, some run, some catch, some throw. All totally different things, but when organized and orchestrated, they achieve something with a purpose. Well guess what? The same goes for business applications. Funnily enough, they're also made up from a bunch of individual or small services, at least the modern cloud-native ones are. But when all of these different individual services are orchestrated, they come together as a useful app.

Okay, just about any modern app out there, certainly a production-worthy one, is going to be composed of multiple interlinked services that span multiple hosts, maybe even span multiple datacenters or clouds. And as soon as we start talking about lots of these apps, so each with lots of independent parts and requirements, we can easily be talking hundreds or thousands of containers with really complex architectures. And at scaling complexity like that, believe me, we do not want to be calling the shots manually. So, for starters, we need a game plan, something that describes how everything in the app fits and works together. Things like, well, first of all, just defining the different services that make up the app, but as well, where they should be deployed and at least how they talk to each other. So networking, message queues, APIs, all of that, it all needs describing in the game plan. And please, I'm sure you get this, but make sure that game plan does not just exist inside your own head or the head of one of your employees. It needs to exist in a system, and we'll come to that in a minute. Anyway, once the app or the game plan is described, we need a way of executing on it, and we normally use the terms deploying and managing. And like we just said, it cannot be manually, not when we get to scale. Now, look, I know that this is high level, but what we've talked about there really is at the core of container orchestration. Define our app, how all the parts interact, provision the infrastructure, and then deploy and manage the app. That's orchestration. But it gives us great things. I mean, dependencies like ordered startup, scheduling services next to each other, or maybe some shouldn't be next to each other, so not starting on the same nodes as others or maybe not even in the same zone or whatever. All of this gets documented in the game plan. Then we give the game plan to an orchestrator, usually that's going to be Kubernetes, and we let the orchestrator deploy the app and manage it. So if usage ramps and we need more web servers or whatever, no sweat. Update the game plan, and the orchestrator makes it happen. It really is good stuff. Now, the main orchestrator out there is Kubernetes, and it is the absolute business. I mean, it's pretty much industry standard, and it does just about everything, but it is big, and the learning curve can be steep. But a smaller and simpler product is Docker Swarm. Now, at its core, it essentially does the same thing, deploy and manage microservices apps. It's just got a lot less features and a lot less momentum. And that's not me knocking it. I'm actually a big fan. It's really simple to use. It's just I think Kubernetes has the brighter future, and I think Docker, Inc. 's own adoption and support of Kubernetes is testament to that.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Orchestration.png?raw=true)

A quick refresher then. We talked about how modern apps are generally composed of multiple services. Think web service, search service, catalog, shopping cart, database, all that goodness. And they all work together, and we get a useful application. Well, generally speaking, each of these individual services runs in its own container, and if we need to scale one of the services, we just throw more containers at it, which is important actually. We don't make the container bigger to cope with demand, we just enlist more of the same container, and then the reverse if we need to scale down; we just take away some of the containers.

Things get complicated, really complicated. I mean, lots of services, many of which need to talk to each other, some need to live next to each other, some absolutely can't live next to each other, things need to scale up and down with business needs and the likes, and before you know it, you need a system just to manage everything. Well, that system is your orchestrator, and it's probably going to be Kubernetes.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Orchestration_2.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Orchestration_3.png?raw=true)

## Instalation

There literally are loads of ways and places to install Docker. There's Windows, there's Mac, there's obviously Linux, but then there's on-premises, there's cloud infrastructure platforms, there's manual install, scripted, wizard-based. There are flipping loads, and we can't tackle them all here.

There are desktop, server and cloud installs.

For desktop - It's all about getting a small Docker environment up and running locally on your Windows laptop or desktop.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Windows.png?raw=true)

What you're going to end up with at the end of this clip is a fully working single engine Docker environment on your Windows desktop or lappy. But it's really only for test and dev work. You're definitely not going to want to run your production estate on it. I mean, it's only a single engine, remember, so it's not scalable or anything. And you might even find that not all features work straightaway. The guys at Docker are very much taking a stability-first feature-second approach here. Although this is Docker for Windows, what you're going to get here is the Docker Engine, which is just a fancy way of saying Docker or the Docker server, but it's going to be running on Linux. Now, stay with me here. Linux inside of a Hyper-V virtual machine.

This is Docker for Windows, but we're getting a Hyper-V virtual machine running a Linux VM called MobyLinuxVM, and inside of that we're going to run Docker.

Despite all of the Linux and virtualization magic going on behind the scenes, what you're going to end up with is the ability to fire up a command prompt or PowerShell, if that's your thing, and just work in Docker commands, and it's going to work. It'll be just like you've got a fully working Docker Engine locally. The only thing to note is that it's running on Linux behind the scenes, and that obviously means you can only run Linux containers on it.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Containers_Deeper.png?raw=true)

The point is, irrespective of platform and operating system, you're going to get the exact same Docker experience.

The docker version is broken into two sections, client and server. At the top here we get the version details of the client bits and pieces, and then down here we get the same for the server or the daemon bits. And if you've been following along with the install lesson, then these are the client and server versions running locally on the machine that you're logged onto. I'm saying this, right, because it is possible to point the Docker client to a remote daemon somewhere on the other end of the network. We're just not doing that here.

With docker info we can see how many containers and images that we've got. Okay, not a lot right now. Then, below that, we've got a wealth of version information. So, generally speaking, right, this is a really good command for seeing how things are on your Docker host.

To run a container we do docker run "name of the container"

First up, right, we typed a command, docker run hello-world. Well, all Docker commands start with the docker keyword, if you will, calling the Docker binary in the background, yeah? We then said run. That's the standard way of saying, hey, Docker, go run me a new container please. And then we said, oh, you know what? Run that container based on the hello-world image. Then we hit Return. The client went and talked to the daemon. The daemon checked if it had a copy of that hello-world image stored locally. It didn't, so that's what we see here with this Unable to find image 'hello-world:latest' locally. That meant the daemon had to go away and pull the image from a place called Docker Hub. More on that in a second. So it pulls the image, which is just a fancy way of saying it grabs its own copy, yeah, and it used that image as a template to create a new container. Now, it's a really simple container, yeah, or image, right? All it did was print a load of text to the screen. Then it exited. So it was a super short-lived container. Prints text to the screen, exits. Meaning, if we run a docker ps command, right, no containers currently running. Though if we whack the -a flag onto the end of that, see how it shows us a container that was running, but is now exited. Well, that's obviously our container that we just ran. And that, right, is a command that you're going to probably use a lot, docker ps, list running containers. Remember, slap -a onto the end, and you can see containers that you have ran, but are now exited. So if we now run docker info again, back up here at the top, yeah, we now see one container, one in a stopped state, and one image. 

docker images shows the local images

## Theory of pulling and running containers

this here is our Docker host. Linux, Windows, on-prem, in the cloud, we don't care, do we? Well, it's running the Docker client and the Docker daemon. You'll often hear that combo referred to as the Docker Engine, though sometimes Docker Engine might just be used to refer to the daemon part. Either way, a standard Docker install gives you the client and the daemon on the same host. Okay, we issued a dead simple docker run command. That was the client component. It interpreted that command and made the appropriate API calls to the daemon. So, right there, we learn that it's obviously the Docker daemon that implements the Docker remote API. Again, sometimes you might hear that called the Docker Engine API. Either way, right, it's a client-server model. Now docker run basically says start me a new container, and then we had to specify the image that we want to use as the template for the container. We said use the hello-world image. So, the daemon checked its local store to see if it already had a copy of it. In our case, it did not. So, what to do? Well, it needs the image in order to start the container. So, what it did was it went and searched for it on Docker Hub. Now, Docker Hub is what we call a Docker image registry, a place, right, where we can store images that we want to use later for containers. Well, Docker Hub's the default registry that the daemon uses, and it's out there on the public internet though other registries totally exist, including secure on-premises registries like Docker Trusted Registry, plus other public and private offerings from third-party ecosystem partners. The point being, the Docker daemon didn't have the required image locally, so it went and searched a registry for it. It found it and pulled it locally. Remember, pull is just Docker lingo for making a local copy. Once the image was pulled locally, the daemon did the heavy lifting needed to create and spin up a brand-new container based, of course, on the configuration inside of the hello-world image. Now, cutting a long story short, that effectively translated into starting a new container that ran a simple command to display some text to the screen, including the words hello world. And as that was the only thing the hello-world image was configured to do, the container did that job, and it exited. That left us with a local copy of the hello-world image in our Docker host's local registry. Again, that's a fancy way of saying on the Docker host's local file system. Plus, we also ended up with a single container in the exited or stopped state.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Containers_Run.png?raw=true)

Just think of images as stopped containers, then, on the flip side, containers as running images.

docker pull to download images

docker rmi to remove images. We can use the name with tag, name or ID

## Container Lifecycle

When you create a container with docker run, it goes into the running state. From there, we can stop it, restart it, stop it, restart it, stop it. I think you get the picture. But also, we can remove it. I mean, look, we can pause them and what have you as well, but as far as we're concerned, containers can be started, stopped, restarted ad infinitum, and ultimately removed. The thing is though, when you stop a container, it's not like it's gone forever, wiped out of existence. No, it's still there, along with its data on the Docker host. So, if and when you restart it, shock, horror, it's going to come back to life with all of its data intact. You see, it's not until you explicitly remove it with a docker rm command that you stand any chance of losing it. So seriously, you literally have to go weapons hot, fire at will, and intentionally wipe it out of existence before you stand a chance of losing it and losing its data. And even then, if its data is in a volume or some other persistent store, that data is not going to go away without a fight. My point being, in a lot of ways, containers are just like VMs. You start them, stop them, restart them. Everything's good. It's not until you explicitly destroy or delete them that you risk losing them.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Containers_Lifecycle.png?raw=true)

docker run -d --name web -o 80:8080 nameoftheimage

-d tells the daemon to start the container in detached mode. Basically, throw it in the background, and don't latch it on to my terminal here.

--name web is to give the name "web" to the container, it must be unique

Then we're mapping some network ports. Now, this particular container, you don't know this, I know that, right, but it's a web server listening on port 8080. Well, we want to be able to access it from port 80 on the Docker host. So, this 80:8080 business is saying map port 80 on the Docker host to port 8080 inside of the container. That means in a second, when we point the web browser to our Docker host on port 80, it's going to get mapped through to port 8080 inside the container, and we should hopefully see a web page.

Top level images are official, the ones with namespaces are not

-it instead of it being detached in the background, I actually want to interact with this one. So I'm saying open its standard in stream and assign it a terminal.

I am now root at this funky hex string. How come? Well, I'm actually in the Bash shell inside the container we just created,

So if we go ping google.com, oh. Vim /etc/hosts. Okay, hang on. What's going on? Well, we're inside of that container, and containers are, for the most part, designed to be super lightweight. So even though this is an Ubuntu container, it's like bare bones, stripped down, furniture removed, and all the fat sucked out. The thing is, right, for the most part, you don't want to be SSHing into containers or anything and doing management stuff from them. I'm just showing you here that it is possible to have interactive containers and to get shell access into them. It's just, it's just not that common.

So containers are very often single process constructs. This particular container, we told it to run /bin/bash, the standard Linux shell if you're a Windows person. And guess what? It is running Bash. It's just that it's running Bash and only Bash. Lightweight remember. But because I'm in that Bash shell right now, if I type exit, then Bash is going to exit. And as that's the only process running on the container, well, that'd leave the container with no running processes, and the container would exit.

And if we go docker PowerShell, okay, there it is still running along with the web container. Now, just real quick, things are a little bit different with Windows Server containers. They've got a bit more going on inside of them. It's just the way that the Windows kernel works. So, Windows containers will likely show more than a single process.

docker stop $(docker ps -aq) - So, the -a here after docker ps says give us all containers, and the q says be quiet, so just return container IDs, effectively giving all container IDs to the docker stop command.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Commands.png?raw=true)

## Swarm Mode

A collection of Docker engines joined into a cluster is called a swarm.

And then the Docker engines running on each node in the swarm is set to be running in swarm mode.

Swarm mode is entirely optional.

But we don't have to, and if we don't, well, that's fine. Docker is just going to run like it always has in standalone mode or single-engine mode. And in that single-engine mode, it is fully backwards compatible. Now let me just repeat that. You do not have to enable swarm mode, and if you don't, then everything is 100% backwards compatible. It just runs like Docker always has. That means if you've got third-party clustering stuff going on, chillax; it's just going to keep working

A swarm itself consists of one or more manager nodes and then one or more worker nodes.

As you might expect, the manager nodes look after the state of the cluster and dispatch tasks and the likes to the worker nodes. And managers are highly available, meaning that if one or two or however many of them go away, the ones remaining will keep the swarm going.

And it works like this behind the scenes. While you can have X number of managers, an odd number is highly recommended, and only one of them, right, is ever the leader or primary. Yeah, all managers maintain the state of the cluster, but if a manager that's not the leader receives a command, it's going to proxy that command over to the leader, and then the leader is going to action it against the swarm. And you can spread managers across availability zones, regions, data centers, whatever suits your high availability needs, but, of course, that's all going to be dependent on the kind of networks that you have. You're going to want reliable networks. Now, I suppose I should mention Raft, because while we can have multiple managers for redundancy and high availability, that naturally gives us complexities over agreeing on the current state, consensus, yeah? Well, Raft is the protocol that's used behind the scenes to bring order to that chaos by ensuring we achieve a distributed consensus. Now, manager nodes are worker nodes as well, so it's totally possible to have a swarm where every node's a manager node, though more than five manager nodes generally isn't thought of as a good idea. The premise here is that the more managers you have, the harder it is to get consensus, or the longer it takes to get consensus. You know what? Just the same way as 2 or 3 people deciding which restaurant to go to is way easier than, I don't know, 20 people.

worker nodes, on the other hand, just accept tasks from managers and execute them. Speaking of which, that leads us nicely to services. So, services are also a new concept introduced with swarm mode, meaning if you're not running in swarm mode, then you can't do services. But we're going to be running swarm mode, so a service, right, is a declarative way of running and scaling tasks. I'm sure that's clear as mud. So, as an example, say you have an app that consists of a back-end store and a front-end web interface. You'd implement that as two services, one service for the back-end store and another for the web front end. Only with services, we tell Docker what we want the app service to look like, and it's now up to Docker to make sure that happens. So let's say we wanted five instances of the container that was running the web front end. You tell Docker that when you define the service.

This is an example command here. So it's telling Docker go create a service called web-fe, and I want 5 instances of the container or task it's going to run. Marvelous. Docker is going to go away and spin up five tasks. Think of tasks as containers for now, okay? And it's going to spread them across all the worker nodes in the cluster. Remember, managers are also workers. But here's the thing, right? It is going to make sure that there are always five of them running. If one of them dies, there's a reconciliation loop running in the background that'll say, okay, I've got four tasks running for this service. Wait up, I should have five, and it's going to start a new one. And that declarative model of expressing desired state and having Docker keep an eye on things making sure that actual state always equals desired state is both new and really powerful.

So, a task is the atomic unit of work assigned to a worker node. We, as developers or sysadmins or whatever, tell the manager about services. Then the manager assigns the work of that service out to worker nodes as tasks. Now, right now, tasks means containers. Okay, a little bit more, so they include metadata about how to initiate the container and some runtime stuff, right, but we can pretty much think of a task as being a container. But, and this is crystal ball time here, right, there's actually nothing technically stopping tasks, including other things like unit kernels or any other unit of work in the future. It's just right now tasks mean containers.

we've got a swarm consisting of a bunch of manager nodes and worker nodes. We define services, declare them to the manager via the standard Docker API, albeit new endpoints in the API. The manager then splits the service into tasks and schedules those tasks against available nodes in the swarm. And then, in order to deploy complex apps consisting of multiple distributed independently scalable services, we've got stacks and distributed application bundles.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Swarm.png?raw=true)

First up, advertise-addr. This tells Docker no matter how many NICs and IP addresses this machine's got, this is the one to use for swarm-related stuff, like exposing the API. Now, if the machine's only got a single IP, okay, you don't technically need to specify this, but how many production machines are there in the real world that have only got a single IP? Not so many in my experience, so I always use this flag and the next one as well, right, so I recommend you do. In fact, I've had issues just this last week in a setup with multiple IPs where I was trying to do something really quickly, so I skipped the flags. Hmm, just said I always use them.

we give it its IP, and we'll use port 2377. Then I give it the listen-addr as well. This is what the node listens on for swarm manager traffic. And I'll tell it the same address and port. Now, for clarity, the addresses used here, well, obviously they need to be valid addresses on the node, but any other nodes that want to be part of this swarm are going to need to be either on the same network, or you're going to need roots or routes in place on your network so that those other nodes can reach this IP. Now, 2377 is not mandatory. You can actually go with any port that works in your environment. But the native Docker Engine port is 2375, the secured engine port 2376, and I think the guys at Docker are talking to IANA trying to register 2377 as the official swarm port. So, long story short, if 2377 works for you in your environment and you're looking to standardize on something, it's as good a place as any to start.

docker swarm join --token SWMTKN-1-5g7uct1fcmv3xqy7beb257wlvhz1x8h8yuuci0n6jln6uv1fuq-1rm2i6zxomnyr0iu8c4a5p23k 192.168.65.3:2377

This command here is the exact command you need to run on a worker that you want to join to this swarm. If you look at it, we can see it includes a token that without which no machines are going to be joining as a worker.

docker swarm join-token manager

But before that, the instruction here to add a manager, I think it's a bit, well, it's a bit weird, right? So, let's grab it, and we'll run it. And look at that. We get another command that looks pretty much the same as the one above, only it's not, well, the token's not the same. But let's do this first. Same command again, but switch out manager for worker. Right. So, any time you need to know the command and token to join a worker or a manager, these are the commands, docker swarm join-token and then either manager or worker. But look at the last section of each token and see how they're different. When joining a new node to the swarm, the way that the swarm knows whether it should be a manager or a worker totally depends on the token that you give it. One token is for managers to join, the other for workers.

You can only run docker node ls from a manager

Always put the same address

Docker node promote - promotes a worker to a manager

The managers are the ones that have got something in the MANAGER STATUS column at the end.

## Services

Services, one of the major, major constructs introduced in 1.12, and they're all about simplifying and, I don't know, robustifying large-scale production deployments.

Services are all about declarativeness, but also the concepts of desired state and actual state with a form of reconciliation process running in the background doing all the heavy lifting in the background required to make sure that actual match is desired. Anyway, we manage services with a new subcommand.

docker serivce create --name X -p 8080:8080 --replicas Image

docker service ls

docker service ps nameOfService

Every task or container gets its own unique ID. Then, fortunately, it also gets a friendlier name, which is basically the name of the service that it belongs to, and then it gets a task number added to the end. So service name dot task number, or I sometimes hear people call it slot number. Well, then we see an image name. All tasks in a service use the same image, I guess, unless you're doing a rolling update or something, which we're going to see shortly. We can see which node a task's running on. And actually, you might notice ours are nicely balanced across the swarm. Then we see the desired state of the task and the actual state. They match. Okay, the world is a happy place. Then there's a column at the end for errors.

docker service inspect 

This is good for drilling into the config of your service. We can see things like, down here, the image we used, then a bunch of settings that we didn't bother with. But further down here, and I think you'll find this important in the real world, then network config. Now, actually, yeah, I should have said this. It's really easy, and you know what? Most of the time I always do this. I don't know why I didn't do it this time. I usually start my services on their own overlay network just because overlay is the future of networking, right?

I want to see what the app looks like in the real world. Well, when we exposed port 8080 for the service, we basically said any traffic hitting any node in the swarm on that port is going to reach our service.

So all I've done here is I've taken the IP, or actually, I've taken the DNS name, yeah, but I've taken the IP or the DNS of any of the nodes in the swarm. Then I've hit it on port 8080. Now, if you're running something more locally, maybe like on your laptop or a physical server on-prem or something, you just need to hit it on whatever IP or DNS it resolves to. Now, if you're logged on to it locally, you can even hit localhost or 127.0 .0 .1 or whatever, right? As long as you're hitting it on port 8080. Now, the reason for port 8080 here is just because that's the way I built this particular app, and it's how we define the service. So hopefully that's clear.

Now, we're hitting the service on mgr1 here, and we know that mgr1 is running a task or a container for the service. So what would happen if we hit a node in the swarm that's not running a task for the service?

Now this is despite the fact, and let me be really, really clear here, despite the fact that we just hit the one and only node in the swarm that does not have a container running for the service. So we literally can hit any node in the swarm, and we'll always get to our service.

Then, as we saw a second ago, any time you hit any node in the swarm on that port you're going to get your service. But as well as that, you're going to get your traffic load balanced across all of the tasks that form the service. A fully container-aware load balancer that Docker are calling the rooting mesh, or the routing mesh. And you can totally mix it with your traditional load balancer.

But whatever environment you're in, if you've got existing load balancers, you can still have them in the mix. So they would load balance traffic across the nodes in your swarm. Then it would let swarm, as the next layer, use the built-in routing mesh load balancer to further balance the load across the containers in the service.

So we spun up a new service. I asked for five instances of the container in the service. We've got six available workers. So, five of those workers got a container, and one didn't. Boohoo! I also said map port 8080 on the entire swarm to 8080 inside of each container that's forming part of the service. So, up here against the entire swarm, all six nodes, even though one of them isn't even carrying a load for the service at the moment, we get a port mapping, so taking 8080 coming in and mapping it through 8080 on each of these containers. Now, I'm only showing that mapping on the end node here so that the diagram doesn't get even uglier, but effectively, every node gets this mapping here. Now, the details are a little bit beyond this course. I'm hoping at some point I'll get around to doing a swarm mode networking course where we can get into all the lovely details of kernel IPVS, sandboxes, ingress networks, VX LANs, all that good stuff. But for now, what we need to know now is we can hit any of these nodes in the swarm on 8080 and get sent to a container that's running as part of the PS1 service and all nicely load balanced. That means any external load balancers down here, they can balance across all the nodes, even this one here that doesn't have a container for the service, and then the swarm-wide routing mesh container-aware load balancer will balance across all of the containers in the service.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Docker_Service.png?raw=true)

## Scaling Services

Actual state, much as desired state, five out of five up here, and then all five down here are running and should be running.

When a node goes down docker gets one back up again and there is no need for manual intervention.

docker service scale nameOfTheService=X

or

docker service update --replicas

If we have, for example, 5 nodes and we scale our app to 10, every node is going to have 2 containers

Newly added nodes don't get existing tasks automatically rebalanced across them.
