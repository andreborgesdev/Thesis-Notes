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

